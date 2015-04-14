using AspNetKoValidation.RuleConfigurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.Tests
{
	[TestClass]
	public class RuleConfigurationTest
	{
		[TestMethod]
		public void SimpleRuleConfigurationTest()
		{
			string ruleName = "TestRule";
			object parameters = new object();
			
			var sut = new SimpleRuleConfiguration(ruleName, parameters);

			Assert.AreEqual(ruleName, sut.RuleName);
			Assert.AreEqual(parameters, sut.Parameters);
		}

		[TestMethod]
		public void RuleWithMessageConfigurationCreatesProperParameters()
		{
			string ruleName = "TestRule";
			object parameters = 123;
			string message = "Test Message";

			var sut = new RuleWithMessageConfiguration(ruleName, parameters, message);
					

			var expectedParametersToSerialize = new
			{
				@params = parameters,
				message = message
			};

			var expectedParametersSerialized = JsonConvert.SerializeObject(expectedParametersToSerialize);
			var parametersSerialized = JsonConvert.SerializeObject(sut.Parameters);

			Assert.AreEqual(ruleName, sut.RuleName);
			Assert.AreEqual(expectedParametersSerialized, parametersSerialized);
		}
	}
}
