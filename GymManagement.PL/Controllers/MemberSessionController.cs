using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.PL.Controllers
{
    public class MemberSessionController : Controller
    {
        public MemberSessionController()
        {

        }

        [HttpGet]
        // GET: MemberSession
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: MemberSession/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // POST: MemberSession/Create
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
    }
}
