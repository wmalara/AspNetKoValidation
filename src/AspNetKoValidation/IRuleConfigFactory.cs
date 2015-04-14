using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation
{
	public interface IRuleConfigFactory
	{
		IEnumerable<IRuleConfiguration> GetRuleConfigurations(ValidationAttribute attribute);
	}
}
