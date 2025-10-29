using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface IMemberService
    {
        ViewResponse<IEnumerable<MemberViewModel>> GetAllMembers();
        ViewResponse<MemberViewModel> CreateMember(CreateMemberViewModel createModel);
        ViewResponse<MemberViewModel> GetMemberById(int id);
        ViewResponse<MemberDetailsViewModel> GetMemberDetailsById(int id);
        ViewResponse<HealthRecordViewModel> GetHealthRecordByMemberId(int id);
        ViewResponse<UpdateMemberViewModel> GetMemberByIdForUpdate(int id);
        ViewResponse<MemberViewModel> GetMemberByEmail(string email);
        ViewResponse<MemberViewModel> GetMemberByPhone(string phone);
        ViewResponse<MemberViewModel> UpdateMember(int id, UpdateMemberViewModel updateModel);
        ViewResponse<MemberViewModel> DeleteById(int id);
    }
}
