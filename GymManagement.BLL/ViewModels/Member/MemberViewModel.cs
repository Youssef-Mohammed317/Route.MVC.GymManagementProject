using GymManagement.DAL.Entites;
using GymManagement.DAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Member
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Gender { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string PhotoName { get; set; } = null!;
    }

}
