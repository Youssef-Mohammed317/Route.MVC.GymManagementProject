using System;
using System.ComponentModel.DataAnnotations;
using GymManagement.BLL.Interfaces;

namespace GymManagement.PL.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult("Email is required.");

            var email = value.ToString();

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

            var memberResponse = memberService.GetMemberByEmail(email!);
            if (memberResponse.IsSuccess)
            {
                bool isSameEntity = currentEntityType.Contains("member");
                if (!isSameEntity || memberResponse.Data!.Id != currentId)
                {
                    return new ValidationResult("Email already exists in members.");
                }
            }

            var trainerResponse = trainerService.GetTrainerByEmail(email!);
            if (trainerResponse.IsSuccess)
            {
                bool isSameEntity = currentEntityType.Contains("trainer");
                if (!isSameEntity || trainerResponse.Data!.Id != currentId)
                {
                    return new ValidationResult("Email already exists in trainers.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
