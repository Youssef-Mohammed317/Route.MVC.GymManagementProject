using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Email is required.");

            var email = value.ToString();

            var memberServiceType = typeof(IMemberService);
            var memberService = (IMemberService?)validationContext.GetService(memberServiceType);

            if (memberService is null)
                throw new InvalidOperationException("IMemberService is not available.");

            var response = memberService.GetByEmail(email!);

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
                return new ValidationResult("Email already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
