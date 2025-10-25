using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.ViewModels.Membership
{
    public class CreateMembershipViewModel
    {
        public int PlanId { get; set; }
        public int MemberId { get; set; }
        public IEnumerable<PlanSelectModel> Plans { get; set; } = Enumerable.Empty<PlanSelectModel>();
        public IEnumerable<MemberSelectModel> Members { get; set; } = Enumerable.Empty<MemberSelectModel>();


    }
}
