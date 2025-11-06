using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class AllowedPhotoMaxSizeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("File is required.");

            }

            const long maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB

            var fileSize = ((IFormFile)value).Length;

            if (fileSize > maxFileSizeInBytes)
            {
                return new ValidationResult($"File size exceeds the maximum allowed limit of {maxFileSizeInBytes / (1024 * 1024)} MB.");
            }
            return ValidationResult.Success;
        }
    }
}
