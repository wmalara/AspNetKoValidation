using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AspNetKoValidation.Tests
{
	[TestClass]
	public class KnockoutValidationTest
	{
		[TestMethod]
		public void RequiredNoMessagePropertyTest()
		{
			var model = new RequiredNoMessagePropertyViewModel();
			var koValidation = new KnockoutValidationGenerator();

			var generatedValidatonJson = koValidation.Generate(model, JsonConvert.SerializeObject);

			var objectToSerialize = new
			{
				SomeProperty = new
				{
					required = true
				}
			};
			var expectedJson = JsonConvert.SerializeObject(objectToSerialize);
			
			Assert.AreEqual(expectedJson, generatedValidatonJson);
        }

		[TestMethod]
		public void RequiredHardCodedMessagePropertyTest()
		{
			var model = new RequiredHardCodedMessagePropertyViewModel();
			var koValidation = new KnockoutValidationGenerator();

			var generatedValidatonJson = koValidation.Generate(model, JsonConvert.SerializeObject);

			var objectToSerialize = new
			{
				SomeProperty = new
				{
					required = new
					{
						@params = true,
						message = RequiredHardCodedMessagePropertyViewModel.ErrorMessage
					}
				}
			};
			var expectedJson = JsonConvert.SerializeObject(objectToSerialize);

			Assert.AreEqual(expectedJson, generatedValidatonJson);
		}

		[TestMethod]
		public void RequiredResourceMessagePropertyTest()
		{
			var model = new RequiredResourceMessagePropertyViewModel();
			var koValidation = new KnockoutValidationGenerator();

			var generatedValidatonJson = koValidation.Generate(model, JsonConvert.SerializeObject);

			var objectToSerialize = new
			{
				SomeProperty = new
				{
					required = new
					{
						@params = true,
						message = Resources.ResourceManager.GetString(RequiredResourceMessagePropertyViewModel.ErrorMessageResourceName)
					}
				}
			};
			var expectedJson = JsonConvert.SerializeObject(objectToSerialize);

			Assert.AreEqual(expectedJson, generatedValidatonJson);
		}

		[TestMethod]
		public void NotRegisteredNewAttributeRuleConfigFactoryGivesEmptyRule()
		{
			var viewModel = new TestValidationAttributePropertyViewModel();

			var koValidation = new KnockoutValidationGenerator();

			var generatedValidatonJson = koValidation.Generate(viewModel, JsonConvert.SerializeObject);

			var objectToSerialize = new
			{
				SomeProperty = new { }
			};

			var expectedJson = JsonConvert.SerializeObject(objectToSerialize);

			Assert.AreEqual(expectedJson, generatedValidatonJson);
		}

		[TestMethod]
		public void DefaultConfigRegisteredNewAttributeRuleConfigFactoryTest()
		{
			RegisterNewAttributeRuleConfigFactoryTestCore(true);
		}

		[TestMethod]
		public void EmptyConfigRegisteredNewAttributeRuleConfigFactoryTest()
		{
			RegisterNewAttributeRuleConfigFactoryTestCore(false);
		}

		private static void RegisterNewAttributeRuleConfigFactoryTestCore(bool useDefaultConfiguration)
		{
			var viewModel = new TestValidationAttributePropertyViewModel();

			var koValidation = new KnockoutValidationGenerator(useDefaultConfiguration);
			koValidation.RegisterRuleConfigFactory(new TestRuleConfigFactory(), typeof(TestValidationAttribute));

			var generatedValidatonJson = koValidation.Generate(viewModel, JsonConvert.SerializeObject);

			var objectToSerialize = new
			{
				SomeProperty = new Dictionary<string, object>
				{
					{ TestRuleConfigFactory.RuleName, TestValidationAttributePropertyViewModel.AdditionalPropertyValue }
				}
			};
			var expectedJson = JsonConvert.SerializeObject(objectToSerialize);

			Assert.AreEqual(expectedJson, generatedValidatonJson);
		}
	}
}
