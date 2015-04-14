using AspNetKoValidation.RuleConfigurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.RuleConfigFactories
{
	public abstract class AbstractRuleConfigFactory<TAttribute> : IRuleConfigFactory
		where TAttribute : ValidationAttribute
	{
		protected TAttribute attribute;

		public virtual IEnumerable<IRuleConfiguration> GetRuleConfigurations(ValidationAttribute alidationAttribute)
		{
			attribute = alidationAttribute as TAttribute;

			if (attribute == null)
				throw new ArgumentException("Invalid attribute type", "attribute");

			var configurations = GetRuleNamesAndParameters().Select(np => GetRuleConfiguration(np.Key, np.Value));

			return configurations;
		}

		protected virtual IRuleConfiguration GetRuleConfiguration(string ruleName, object parameters)
		{
			IRuleConfiguration ruleConfiguration;

			if (ErrorMessageHelper.HasCustomMessage(attribute))
			{
				var message = ErrorMessageHelper.GetErrorMessage(attribute);
				ruleConfiguration = new RuleWithMessageConfiguration(ruleName, parameters, message);
            }
			else
			{
				ruleConfiguration = new SimpleRuleConfiguration(ruleName, parameters);
			} 

			return ruleConfiguration;
		}
		
		protected abstract IEnumerable<KeyValuePair<string, object>> GetRuleNamesAndParameters();
	}
}
