using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Membership
{
    public class MembershipViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
        }
        public int MemberId { get; set; }
        public int PlanId { get; set; }
    }
}
