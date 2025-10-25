using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.SessionViewModel;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;
        private readonly ICategoryService categoryService;
        private readonly ITrainerService trainerService;

        public SessionController(ISessionService _sessionService,
            ICategoryService _categoryService,
            ITrainerService _trainerService)
        {
            sessionService = _sessionService;
            categoryService = _categoryService;
            trainerService = _trainerService;
        }
        [HttpGet]
        // GET: Session/Index
        public IActionResult Index()
        {
            var response = sessionService.GetAllSessions();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }
        [HttpGet]
        // GET: Session/Details/5
        public IActionResult Details([FromRoute] int id)
        {
            var response = sessionService.GetSessionById(id);
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
        // GET: Session/Create
        public IActionResult Create()
        {
            ViewBag.Categories = categoryService.GetAllCategories().Data;
            ViewBag.Trainers = trainerService.GetAllTrainers().Data;
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View();
        }

        [HttpPost]
        [ValidateModel]
        // POST: Session/Create
        public IActionResult Create([FromForm] CreateSessionViewModel model)
        {
            var response = sessionService.CreateSession(model);

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

        [HttpGet]
        // GET: Session/Edit/5
        public IActionResult Edit([FromRoute] int id)
        {
            var response = sessionService.GetSessionByIdForUpdate(id);
            ViewBag.Trainers = trainerService.GetAllTrainers().Data;
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

        [HttpPost]
        [ValidateModel]
        // POST: Session/Edit/5
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdateSessionViewModel model)
        {

            var response = sessionService.UpdateSession(id, model);

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

        [HttpGet]
        // GET: SessionController/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var response = sessionService.GetSessionById(id);
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

        [HttpPost]
        // POST: Session/Delete/5
        public IActionResult ConfirmDelete([FromRoute] int id)
        {

            var response = sessionService.DeleteSessionById(id);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return View();
            }
        }
    }
}
