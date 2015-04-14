using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation
{
	public interface IRuleConfiguration
	{
		string RuleName { get; }

		object Parameters { get; }
	}
}
