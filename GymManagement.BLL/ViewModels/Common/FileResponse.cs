using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Common
{
    public class FileResponse
    {

        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }

    }
}
