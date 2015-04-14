using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.RuleConfigurations
{
	public class SimpleRuleConfiguration : IRuleConfiguration
	{
		private string ruleName;
		private object parameters;

		public SimpleRuleConfiguration()
		{
		}

		public SimpleRuleConfiguration(string ruleName, object parameters)
		{
			this.ruleName = ruleName;
			this.parameters = parameters;
		}

		public object Parameters
		{
			get { return parameters; }
			set { parameters = value; }
		}

		public string RuleName
		{
			get { return ruleName; }
			set { ruleName = value; }
		}
	}
}
