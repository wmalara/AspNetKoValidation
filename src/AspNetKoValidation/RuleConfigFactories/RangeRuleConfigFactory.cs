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
	public class RangeRuleConfigFactory : AbstractRuleConfigFactory<RangeAttribute>
	{
		private const string maxRuleName = "max";
		private const string minRuleName = "min";

		protected override IEnumerable<KeyValuePair<string, object>> GetRuleNamesAndParameters()
		{
			yield return new KeyValuePair<string, object>(maxRuleName, attribute.Maximum);
			yield return new KeyValuePair<string, object>(minRuleName, attribute.Minimum);
		}
	}
}
