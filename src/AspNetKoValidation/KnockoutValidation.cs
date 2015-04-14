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
    public static class KnockoutValidation
    {
		public static string Generate<T>(T model, Func<object, string> serializer)
		{
			var generator = new KnockoutValidationGenerator();

			return generator.Generate(model, serializer);
		}
	}
}
