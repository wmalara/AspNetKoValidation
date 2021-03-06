﻿using AspNetKoValidation.RuleConfigFactories;
using AspNetKoValidation.RuleConfigurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.RuleConfigFactories
{
	public class RegularExpressionRuleConfigFactory : AbstractRuleConfigFactory<RegularExpressionAttribute>
	{
		private const string ruleName = "pattern";
		
		protected override IEnumerable<KeyValuePair<string, object>> GetRuleNamesAndParameters()
		{
			yield return new KeyValuePair<string, object>(ruleName, attribute.Pattern);
		}
	}
}
