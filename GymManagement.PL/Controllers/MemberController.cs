using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService _memberService)
        {
            memberService = _memberService;
        }
        // GET: Member/Index
        public IActionResult Index()
        {
            var response = memberService.GetAllMembers();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        // GET: Member/Details/5
        public IActionResult MemberDetails([FromRoute] int id)
        {
            var response = memberService.GetById(id);
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
        // GET: Member/Details/5

        public IActionResult HealthRecordDetails([FromRoute] int id)
        {
            var response = memberService.GetHealthRecordByMemberId(id);
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

        // GET: Member/Create
        public IActionResult Create()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateModel]
        public IActionResult Create([FromForm] CreateMemberViewModel model)
        {
            var response = memberService.CreateMember(model);

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

        // GET: Member/Edit/5
        public IActionResult Edit([FromRoute] int id)
        {
            var response = memberService.GetMemberByIdForUpdate(id);
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

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateModel]
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdateMemberViewModel model)
        {

            var response = memberService.UpdateMember(id, model);

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

        // GET: MemberController/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var response = memberService.GetById(id);
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

        // POST: MemberController/Delete/5
        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {

            var response = memberService.DeleteById(id);

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
