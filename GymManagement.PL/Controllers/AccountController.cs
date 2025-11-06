using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Account;
using GymManagement.DAL.Entites;
using GymManagement.PL.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagement.PL.Controllers
{
    //[AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService _accountService, SignInManager<ApplicationUser> signInManager)
        {
            accountService = _accountService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login(AccountViewModel model)
        {

            var user = await accountService.ValidateUser(model);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Email not allowed!.");
                return View(model);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "User Locked out!.");
                return View(model);
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
