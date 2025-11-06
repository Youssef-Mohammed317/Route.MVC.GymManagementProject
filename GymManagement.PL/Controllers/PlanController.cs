using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
using GymManagement.BLL.ViewModels.Plan;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService _planService)
        {
            planService = _planService;
        }
        [HttpGet]
        // GET: Plan/Index
        public IActionResult Index()
        {
            var response = planService.GetAllPlans();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        [HttpGet]
        // GET: PlanController/Details/5
        public IActionResult Details([FromRoute] int id)
        {
            var response = planService.GetPlanById(id);
            if (response.IsSuccess)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? response.Message ?? "";
                return View(response.Data);
            }
            else
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? response.Message ?? "";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        // GET: PlanController/Edit/5
        public IActionResult Edit([FromRoute] int id)
        {
            var response = planService.GetPlanByIdForUpdate(id);
            if (response.IsSuccess)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? response.Message ?? "";
                return View(response.Data);
            }
            else
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? response.Message ?? "";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PlanController/Edit/5
        [HttpPost]
        [ValidateModel]
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdatePlanViewModel model)
        {
            var response = planService.UpdatePlan(id, model);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return View(model);
            }
        }

        [HttpPost]
        // POST: PlanController/ToggleStatus/5
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var response = planService.TogglePlan(id);
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
