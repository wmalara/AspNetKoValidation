using AspNetKoValidation.RuleConfigFactories;
using AspNetKoValidation.RuleConfigurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.RuleConfigFactories
{
	public class StringLengthRuleConfigFactory : AbstractRuleConfigFactory<StringLengthAttribute>
	{
		private const string maxLengthRuleName = "maxLength";
		private const string minLengthRuleName = "minLength";


		protected override IEnumerable<KeyValuePair<string, object>> GetRuleNamesAndParameters()
		{
			yield return new KeyValuePair<string, object>(maxLengthRuleName, attribute.MaximumLength);

			if (attribute.MinimumLength > 0)
			{
				yield return new KeyValuePair<string, object>(minLengthRuleName, attribute.MinimumLength);
			}
		}
	}
}
