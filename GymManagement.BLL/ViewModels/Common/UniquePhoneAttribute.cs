using GymManagement.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Phone is required.");

            var phone = value.ToString();

            var memberServiceType = typeof(IMemberService);
            var memberService = (IMemberService?)validationContext.GetService(memberServiceType);

            if (memberService is null)
                throw new InvalidOperationException("IMemberService is not available.");

            var response = memberService.GetByPhone(phone!);

            var idProperty = validationContext.ObjectType.GetProperty("Id");
            int currentId = 0;

            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(validationContext.ObjectInstance);
                if (idValue != null)
                    int.TryParse(idValue.ToString(), out currentId);
            }
            if (response.IsSuccess && response.Data?.Id != currentId)
            {
                return new ValidationResult("Phone already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
