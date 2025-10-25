using GymManagement.BLL.ViewModels.Analytics;
using GymManagement.BLL.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IAnalyticsService
    {
        ViewResponse<AnalyticsViewModel> GetAnalytics();
    }
}
