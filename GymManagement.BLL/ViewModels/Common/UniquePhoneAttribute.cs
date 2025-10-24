using GymManagement.BLL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace GymManagement.PL.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Phone is required.");

            var phone = value.ToString();

            var memberService = (IMemberService?)validationContext.GetService(typeof(IMemberService));
            var trainerService = (ITrainerService?)validationContext.GetService(typeof(ITrainerService));

            if (memberService is null || trainerService is null)
                throw new InvalidOperationException("Required services are not available.");

            var currentEntityType = validationContext.ObjectType.Name.ToLower();

            var idProperty = validationContext.ObjectType.GetProperty("Id");
            int currentId = 0;
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(validationContext.ObjectInstance);
                if (idValue != null)
                    int.TryParse(idValue.ToString(), out currentId);
            }

            var memberResponse = memberService.GetByPhone(phone!);
            if (memberResponse.IsSuccess)
            {
                bool isSameEntity = currentEntityType.Contains("member");
                if (!isSameEntity || memberResponse.Data!.Id != currentId)
                {
                    return new ValidationResult("Phone already exists in members.");
                }
            }

            var trainerResponse = trainerService.GetByPhone(phone!);
            if (trainerResponse.IsSuccess)
            {
                bool isSameEntity = currentEntityType.Contains("trainer");
                if (!isSameEntity || trainerResponse.Data!.Id != currentId)
                {
                    return new ValidationResult("Phone already exists in trainers.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
