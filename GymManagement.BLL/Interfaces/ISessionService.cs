using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface ISessionService
    {
        ViewResponse<IEnumerable<SessionViewModel>> GetAllSessions();
        ViewResponse<SessionViewModel> GetSessionById(int id);
        ViewResponse<UpdateSessionViewModel> GetSessionByIdForUpdate(int id);
        ViewResponse<CreateSessionViewModel> GetDataForCreateSession();
        ViewResponse<SessionViewModel> DeleteSessionById(int id);
        ViewResponse<SessionViewModel> CreateSession(CreateSessionViewModel createModel);
        ViewResponse<SessionViewModel> UpdateSession(int id, UpdateSessionViewModel updateModel);

    }
}
