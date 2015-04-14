using AspNetKoValidation.RuleConfigFactories;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation
{
	using ReadonlyFactoriesDictionary = IReadOnlyDictionary<Type, IReadOnlyList<IRuleConfigFactory>>;
	using WriteableFactoriesDictionary = ConcurrentDictionary<Type, IList<IRuleConfigFactory>>;

	public class KnockoutValidationGenerator
    {
		private static readonly Lazy<ReadonlyFactoriesDictionary> defaultFactoriesPrototype = new Lazy<ReadonlyFactoriesDictionary>(CreateDefaultFactoriesPrototype);
        private readonly bool withDefaultConfiguration;
        private readonly object copyDefaultFactoriesLock = new object();
        private WriteableFactoriesDictionary customFactories;		

		public KnockoutValidationGenerator() : this(true)
		{
		}

		public KnockoutValidationGenerator(bool withDefaultConfiguration)
		{
			this.withDefaultConfiguration = withDefaultConfiguration;

			if (!this.withDefaultConfiguration)
			{
				customFactories = new WriteableFactoriesDictionary();
			}
		}

        public static ReadonlyFactoriesDictionary DefaultRuleConfigFactories
        {
            get
            {
                return defaultFactoriesPrototype.Value;
            }
        }

		public string Generate<T>(T model, Func<object, string> serializer)
		{
			var validationDictionary = typeof(T).GetProperties()
					.Select(p => new
					{
						Property = p,
						ValidationAttributes = p.GetCustomAttributes<ValidationAttribute>(true)
					})
					.Where(p => p.ValidationAttributes.Any())
					.ToDictionary(p => p.Property.Name, p => GetRuleConfigurations(p.ValidationAttributes));

			var json = serializer(validationDictionary);

			return json;
		}

		public void RegisterRuleConfigFactory(IRuleConfigFactory factory, Type validationAttributeType)
		{
			if (!typeof(ValidationAttribute).IsAssignableFrom(validationAttributeType))
				throw new ArgumentException("Provided type should be a ValidationAttribute or its subclass", "validationAttributeType");

			AddFactoriesForAttribute(validationAttributeType, factory);
		}

		public ReadonlyFactoriesDictionary RuleConfigFactories
		{
			get
			{
                if(customFactories != null)
                {
                    return customFactories.ToDictionary(cf => cf.Key, cf => (IReadOnlyList<IRuleConfigFactory>)cf.Value);   //we can't just cast to ReadonlyFactoriesDictionary because IReadOnlyDictionary is not covariant
                }

                return DefaultRuleConfigFactories;
			}
		}		

		private static ReadonlyFactoriesDictionary CreateDefaultFactoriesPrototype()
		{
			return new Dictionary<Type, IReadOnlyList<IRuleConfigFactory>>
			{
				{typeof(EmailAddressAttribute), new List<IRuleConfigFactory> { new EmailRuleConfigFactory() } },
				{typeof(MaxLengthAttribute), new List<IRuleConfigFactory> { new MaxLengthRuleConfigFactory() } },
				{typeof(MinLengthAttribute), new List<IRuleConfigFactory> { new MinLengthRuleConfigFactory() } },
				{typeof(RangeAttribute), new List<IRuleConfigFactory> { new RangeRuleConfigFactory() } },
				{typeof(RegularExpressionAttribute), new List<IRuleConfigFactory> { new RegularExpressionRuleConfigFactory() } },
				{typeof(RequiredAttribute), new List<IRuleConfigFactory> { new RequiredRuleConfigFactory() } },
				{typeof(StringLengthAttribute), new List<IRuleConfigFactory> { new StringLengthRuleConfigFactory() } }
			};
		}

		private WriteableFactoriesDictionary WriteableFactories
		{
			get
			{
				if (customFactories == null)
				{
					lock(copyDefaultFactoriesLock)
					{
						if (customFactories == null)
						{
							customFactories = CopyDefaultFactories();
						}
					}
				}

				return customFactories;
			}
		}

		private Dictionary<string, object> GetRuleConfigurations(IEnumerable<ValidationAttribute> validationAttributes)
		{
			var ruleConfigurations = validationAttributes
					.Select(va => new { AttributeType = va.GetType(), Attribute = va })
					.Where(va => RuleConfigFactories.ContainsKey(va.AttributeType))
					.SelectMany(va =>
						RuleConfigFactories[va.AttributeType]
						.SelectMany(rcf => rcf.GetRuleConfigurations(va.Attribute)))
						.Aggregate(new Dictionary<string, object>(), (dict, config) =>
						{
							dict[config.RuleName] = config.Parameters;
							return dict;
						});

			return ruleConfigurations;
		}

		private WriteableFactoriesDictionary CopyDefaultFactories()
		{
			var protoCopy = defaultFactoriesPrototype.Value.ToDictionary(dfp => 
				dfp.Key, 
				dfp => (IList<IRuleConfigFactory>)dfp.Value.ToList());

			return new WriteableFactoriesDictionary(protoCopy);
		}

		private void AddFactoriesForAttribute(Type attributeType, IRuleConfigFactory factory)
		{
			var attributeFactories = WriteableFactories.GetOrAdd(attributeType, at => new List<IRuleConfigFactory>());
			attributeFactories.Add(factory);
		}	
    }
}
