using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Member
{
    public class MemberDetailsViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string PlanName { get; set; } = null!;
        public string MembershipStartDate { get; set; }
        public string MembershipEndDate { get; set; }
        public string Address { get; set; } = null!;
    }
}
