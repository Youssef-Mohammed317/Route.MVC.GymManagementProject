using GymManagement.BLL.ViewModels.Account;
using GymManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser?> ValidateUser(AccountViewModel model);
        Task<ApplicationUser?> Logout(AccountViewModel model);
    }
}
