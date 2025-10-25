using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Trainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Interfaces
{
    public interface ITrainerService
    {
        ViewResponse<IEnumerable<TrainerViewModel>> GetAllTrainers();

        ViewResponse<TrainerViewModel> CreateTrainer(CreateTrainerViewModel createModel);
        ViewResponse<TrainerViewModel> GetTrainerById(int id);
        ViewResponse<TrainerViewModel> GetTrainerByEmail(string email);
        ViewResponse<TrainerViewModel> GetTrainerByPhone(string phone);
        ViewResponse<UpdateTrainerViewModel> GetTrainerByIdForUpdate(int id);
        ViewResponse<TrainerViewModel> UpdateTrainer(int id, UpdateTrainerViewModel updateModel);
        ViewResponse<TrainerViewModel> DeleteById(int id);
    }
}
