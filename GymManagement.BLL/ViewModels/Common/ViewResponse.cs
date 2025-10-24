using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GymManagement.BLL.ViewModels.Common
{
    public class ViewResponse<TData>
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public TData? Data { get; init; }

        public static ViewResponse<TData> Success(TData data, string? message = null) =>
            new ViewResponse<TData> { IsSuccess = true, Data = data, Message = message ?? string.Empty };

        public static ViewResponse<TData> Fail(string message) =>
            new ViewResponse<TData> { IsSuccess = false, Data = default, Message = message };
    }
}
