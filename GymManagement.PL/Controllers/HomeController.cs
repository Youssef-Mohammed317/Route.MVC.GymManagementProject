using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Analytics;
using GymManagement.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagement.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService analyticsService;

        public HomeController(IAnalyticsService _analyticsService)
        {
            analyticsService = _analyticsService;
        }

        public IActionResult Index()
        {
            var response = analyticsService.GetAnalytics();

            return View(response.Data);
        }
    }
}
