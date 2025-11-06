using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService _memberService)
        {
            memberService = _memberService;
        }
        [HttpGet]
        // GET: Member/Index
        public IActionResult Index()
        {
            var response = memberService.GetAllMembers();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        [HttpGet]
        // GET: Member/Details/5
        public IActionResult MemberDetails([FromRoute] int id)
        {
            var response = memberService.GetMemberDetailsById(id);
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

        [HttpGet]
        // GET: Member/Create
        public IActionResult Create()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View();
        }

        [HttpPost]
        [ValidateModel]
        // POST: Member/Create
        public IActionResult Create([FromForm] CreateMemberViewModel createModel)
        {
            var response = memberService.CreateMember(createModel);

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

        [HttpPost]
        [ValidateModel]
        // POST: Member/Edit/5
        public IActionResult Edit([FromRoute] int id, [FromForm] UpdateMemberViewModel updateModel)
        {

            var response = memberService.UpdateMember(id, updateModel);

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
        // GET: MemberController/Delete/5
        public IActionResult Delete([FromRoute] int id)
        {
            var response = memberService.GetMemberById(id);
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
