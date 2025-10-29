using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IMembershipService
    {
        ViewResponse<IEnumerable<MembershipViewModel>> GetAllMemberships();
        ViewResponse<CreateMembershipViewModel> GetDataForCreateMembership();
        ViewResponse<MembershipViewModel> CreateMembership(CreateMembershipViewModel createModel);
        ViewResponse<MembershipViewModel> DeleteMembership(int id);
    }
}
