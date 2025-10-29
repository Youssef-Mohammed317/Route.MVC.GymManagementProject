using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.MemberSession;
using GymManagement.BLL.ViewModels.Session;

namespace GymManagement.BLL.Interfaces
{
    public interface IMemberSessionService
    {
        ViewResponse<IEnumerable<SessionViewModel>> GetMemberSessions();
        ViewResponse<IEnumerable<MemberSelectModel>> GetMembers();
        ViewResponse<IEnumerable<MemberSessionViewModel>> GetMembersForSessionsBySessionId(int id);
        ViewResponse<MemberSessionViewModel> DeleteMemberSession(int id);
        ViewResponse<MemberSessionViewModel> CreateMemberSession(int id, CreateMemberSessionViewModel createModel);
    }
}
