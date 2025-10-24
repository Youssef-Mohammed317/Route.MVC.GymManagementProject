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
        ViewResponse<MemberViewModel> CreateMember(CreateMemberViewModel createMemberViewModel);
        ViewResponse<MemberViewModel> GetById(int id);
        ViewResponse<HealthRecordViewModel> GetHealthRecordByMemberId(int id);
        ViewResponse<UpdateMemberViewModel> GetMemberByIdForUpdate(int id);
        ViewResponse<MemberViewModel> GetByEmail(string email);
        ViewResponse<MemberViewModel> GetByPhone(string phone);
        ViewResponse<MemberViewModel> UpdateMember(int id, UpdateMemberViewModel updateMemberViewModel);
        ViewResponse<MemberViewModel> DeleteById(int id);
    }
}
