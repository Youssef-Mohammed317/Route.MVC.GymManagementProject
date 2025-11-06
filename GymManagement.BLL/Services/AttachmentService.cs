using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class AttachmentService : IAttachmentService
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        public AttachmentService(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        public FileResponse Upload(string folderName, IFormFile file)
        {
            if (folderName == null || file == null)
            {
                return new FileResponse
                {
                    IsSuccess = false,
                    Message = "Folder name or file is null."
                };
            }

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return new FileResponse
            {
                IsSuccess = true,
                Message = "File uploaded successfully.",
                FileName = uniqueFileName,
                FileUrl = $"Images/{folderName}/{uniqueFileName}"
            };

        }
        public FileResponse Delete(string folderName, string fileName)
        {
            if (folderName == null || fileName == null)
            {
                return new FileResponse
                {
                    IsSuccess = false,
                    Message = "Folder name or file name is null."
                };
            }

            var folderPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", folderName);
            var filePath = Path.Combine(folderPath, fileName);
            if (!File.Exists(filePath))
            {
                return new FileResponse
                {
                    IsSuccess = false,
                    Message = "File does not exist."
                };
            }

            File.Delete(filePath);

            return new FileResponse
            {
                IsSuccess = true,
                Message = "File deleted successfully."
            };
        }

    }
}
