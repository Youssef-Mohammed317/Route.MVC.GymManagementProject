using GymManagement.BLL.Interfaces;
using GymManagement.BLL.Services;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.MemberSession;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    [Authorize]
    public class MemberSessionController : Controller
    {
        private readonly IMemberSessionService memberSessionService;

        public MemberSessionController(IMemberSessionService _memberSessionService)
        {
            memberSessionService = _memberSessionService;
        }

        [HttpGet]
        // GET: MemberSession
        public IActionResult Index()
        {
            var response = memberSessionService.GetMemberSessions();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }
        [HttpGet]
        // GET: MemberSession
        public IActionResult GetMembersForOnGoingSessions([FromRoute] int id)
        {
            var response = memberSessionService.GetMembersForSessionsBySessionId(id);
            ViewBag.SessionId = id;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        [HttpGet]
        // GET: MemberSession
        public IActionResult GetMembersForUpCompingSessions([FromRoute] int id)
        {
            var response = memberSessionService.GetMembersForSessionsBySessionId(id);
            ViewBag.SessionId = id;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(response.Data);
        }

        [HttpGet]
        // GET: MemberSession/CreateMemberSession
        public IActionResult CreateMemberSession([FromRoute] int id)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            var response = memberSessionService.GetMembers();
            return View(new CreateMemberSessionViewModel
            {
                Members = response?.Data!,
                SessionId = id
            });
        }

        [HttpPost]
        // POST: MemberSession/CreateMemberSession
        public IActionResult CreateMemberSession([FromRoute] int id, [FromForm] CreateMemberSessionViewModel createModel)
        {
            var response = memberSessionService.CreateMemberSession(id, createModel);

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
        // POST: MemberSession/Delete/5
        public IActionResult DeleteMemberSession([FromRoute] int id)
        {
            var response = memberSessionService.DeleteMemberSession(id);
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }
            return RedirectToAction(nameof(GetMembersForUpCompingSessions), new { id = response?.Data?.SessionId });
        }
        [HttpPost]
        // POST: MemberSession/ToggleAttendance/5
        public IActionResult ToggleAttendance([FromRoute] int id)
        {
            var response = memberSessionService.ToggleAttendance(id);
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }
            return RedirectToAction(nameof(GetMembersForOnGoingSessions), new {id = response?.Data?.SessionId});
        }

    }
}
