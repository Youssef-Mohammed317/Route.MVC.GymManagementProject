using GymManagement.BLL.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IAttachmentService
    {
        FileResponse Upload(string folderName, IFormFile file);

        FileResponse Delete(string folderName, string fileName);
    }
}
