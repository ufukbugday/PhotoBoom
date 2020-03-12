using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PhotoBoom.Endpoint.ViewModel;

namespace PhotoBoom.Endpoint.Attributes
{
	public class MultipleTagAttribute : ValidationAttribute
	{
		public string GetErrorMessage() =>
			$"Multiple tags should be seperated by comma without space and can be used with letters and numbers.";
		protected override ValidationResult IsValid(object value,
			ValidationContext validationContext)
		{
			var model = (PhotoCreateViewModel)validationContext.ObjectInstance;

			var regex = new Regex(@"(?i)^[a-zA-Z0-9,\s]+$");
			var res = regex.IsMatch(model.TagStr);
			
			if (!res)
			{
				return new ValidationResult(GetErrorMessage());
			}

			return ValidationResult.Success;
		}
	}
}