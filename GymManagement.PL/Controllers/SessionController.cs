using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
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

        public SessionController(ISessionService _sessionService)
        {
            sessionService = _sessionService;

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
            var response = sessionService.GetDataForCreateSession();
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View(response.Data);
        }

        [HttpPost]
        [ValidateModel]
        // POST: Session/Create
        public IActionResult Create([FromForm] CreateSessionViewModel createModel)
        {
            var response = sessionService.CreateSession(createModel);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return View(createModel);
            }
        }

        [HttpGet]
        // GET: Session/Edit/5
        public IActionResult Edit([FromRoute] int id)
        {
            var response = sessionService.GetSessionByIdForUpdate(id);
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
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdateSessionViewModel updateModel)
        {

            var response = sessionService.UpdateSession(id, updateModel);

            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return View(updateModel);
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
