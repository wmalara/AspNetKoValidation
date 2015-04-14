using AspNetKoValidation.RuleConfigFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.Tests
{
	public class TestValidationAttribute : ValidationAttribute
	{
		public string AdditionalProperty { get; set; }
	}

	public class TestRuleConfigFactory : AbstractRuleConfigFactory<TestValidationAttribute>
	{
		public const string RuleName = "testRule";

		protected override IEnumerable<KeyValuePair<string, object>> GetRuleNamesAndParameters()
		{
			yield return new KeyValuePair<string, object>(RuleName, attribute.AdditionalProperty);
        }
	}
}
