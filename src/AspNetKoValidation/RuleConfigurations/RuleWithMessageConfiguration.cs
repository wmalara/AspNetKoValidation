using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.RuleConfigurations
{
	public class RuleWithMessageConfiguration : SimpleRuleConfiguration
	{
		public RuleWithMessageConfiguration(string ruleName, object parameters, string message)
		{
			RuleName = ruleName;
			Parameters = new Dictionary<string, object>
			{
				{"params", parameters },
				{"message", message }
			};
		}
	}
}
