using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetKoValidation
{
	public static class ErrorMessageHelper
	{
		public static string GetErrorMessage(ValidationAttribute attribute)
		{
			string message = attribute.ErrorMessage;

			if (HasResourceMessage(attribute))
			{
				var propertyInfo = attribute.ErrorMessageResourceType.GetProperty(attribute.ErrorMessageResourceName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

				message = propertyInfo.GetValue(propertyInfo.DeclaringType, null) as string;
			}

			return message;
		}

		public static bool HasCustomMessage(ValidationAttribute attribute)
		{
			return string.IsNullOrEmpty(attribute.ErrorMessage) == false || HasResourceMessage(attribute);
		}

		public static bool HasResourceMessage(ValidationAttribute attribute)
		{
			return string.IsNullOrEmpty(attribute.ErrorMessageResourceName) == false && attribute.ErrorMessageResourceType != null;
		}
	}
}
