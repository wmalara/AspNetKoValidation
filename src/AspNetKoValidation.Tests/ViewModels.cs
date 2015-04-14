using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation.Tests
{
	public class RequiredNoMessagePropertyViewModel
	{
		[Required]
		public string SomeProperty { get; set; }
	}

	public class RequiredHardCodedMessagePropertyViewModel
	{
		public const string ErrorMessage = "Field is required";

		[Required(ErrorMessage = ErrorMessage)]
		public string SomeProperty { get; set; }
	}

	public class RequiredResourceMessagePropertyViewModel
	{
		public const string ErrorMessageResourceName = "Validation_Required";

		[Required(ErrorMessageResourceName = ErrorMessageResourceName, ErrorMessageResourceType = typeof(Resources))]
		public string SomeProperty { get; set; }
	}

	public class TestValidationAttributePropertyViewModel
	{
		public const string AdditionalPropertyValue = "Test Value";

		[TestValidation(AdditionalProperty = AdditionalPropertyValue)]
        public string SomeProperty { get; set; }
	}
}
