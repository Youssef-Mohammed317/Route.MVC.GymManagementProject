using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Membership;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMembershipService membershipService;

        public MembershipController(IMembershipService _membershipService)
        {
            membershipService = _membershipService;
        }
        [HttpGet]
        // GET: Membership
        public IActionResult Index()
        {
            var response = membershipService.GetAllMemberships();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }


        [HttpGet]
        // GET: Membership/Create
        public ActionResult Create()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            var response = membershipService.GetDataForCreateMembership();

            return View(response.Data);
        }

        [HttpPost]
        [ValidateModel]
        // POST: Membership/Create
        public IActionResult Create([FromForm] CreateMembershipViewModel createModel)
        {

            var response = membershipService.CreateMembership(createModel);

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
        [HttpPost]
        // POST: Membership/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var response = membershipService.DeleteMembership(id);
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
