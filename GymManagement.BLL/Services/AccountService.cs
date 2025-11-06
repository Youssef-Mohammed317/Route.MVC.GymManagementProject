using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Account;
using GymManagement.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        public Task<ApplicationUser?> Logout(AccountViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser?> ValidateUser(AccountViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
                if (passwordValid)
                {
                    return user;
                }
            }



            return null;
        }
    }
}
