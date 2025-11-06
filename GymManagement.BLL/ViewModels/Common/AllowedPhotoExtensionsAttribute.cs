using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class AllowedPhotoExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("File is required.");
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileName = ((IFormFile)value).FileName;
            var fileExtension = Path.GetExtension(fileName!);
            if (!allowedExtensions.Contains(fileExtension!.ToLower()))
            {
                return new ValidationResult($"Invalid file type. Only the following extensions are allowed: {string.Join(", ", allowedExtensions)}");
            }

            return ValidationResult.Success;
        }
    }
}
