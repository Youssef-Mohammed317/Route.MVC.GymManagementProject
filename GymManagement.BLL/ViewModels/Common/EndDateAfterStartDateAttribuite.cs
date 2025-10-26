using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endDate = value as DateTime?;
            var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");

            if (startDateProperty == null)
                return new ValidationResult("StartDate property not found.");

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance);
            if (startDateValue is not DateTime startDate)
                return new ValidationResult("StartDate must be a valid date.");

            if (endDate == null)
                return new ValidationResult("End date is required.");

            if (endDate <= startDate)
                return new ValidationResult(ErrorMessage ?? "End date must be after start date.");

            return ValidationResult.Success;
        }
    }
}