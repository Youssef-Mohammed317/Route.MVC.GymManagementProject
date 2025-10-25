using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Trainer;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService _trainerService)
        {
            trainerService = _trainerService;
        }
        [HttpGet]
        // GET: Trainer/Index
        public IActionResult Index()
        {
            var response = trainerService.GetAllTrainers();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        [HttpGet]
        // GET: Trainer/Details/5
        public IActionResult Details([FromRoute] int id)
        {
            var response = trainerService.GetTrainerById(id);
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
        // GET: Trainer/Create
        public IActionResult Create()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View();
        }

        [HttpPost]
        [ValidateModel]
        // POST: Trainer/Create
        public IActionResult Create([FromForm] CreateTrainerViewModel model)
        {
            var response = trainerService.CreateTrainer(model);

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
        // GET: Trainer/Edit/5
        public ActionResult Edit([FromRoute] int id)
        {
            var response = trainerService.GetTrainerByIdForUpdate(id);
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
        // POST: Trainer/Edit/5
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdateTrainerViewModel model)
        {

            var response = trainerService.UpdateTrainer(id, model);

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
        // GET: Trainer/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var response = trainerService.GetTrainerById(id);
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
        // POST: Trainer/Delete/5
        public IActionResult ConfirmDelete([FromRoute] int id)
        {

            var response = trainerService.DeleteById(id);

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
