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

        ViewResponse<TrainerViewModel> CreateTrainer(CreateTrainerViewModel createTrainerViewModel);
        ViewResponse<TrainerViewModel> GetById(int id);
        ViewResponse<TrainerViewModel>  GetByEmail(string email);
        ViewResponse<TrainerViewModel>  GetByPhone(string phone);
        ViewResponse<UpdateTrainerViewModel> GetTrainerByIdForUpdate(int id);
        ViewResponse<TrainerViewModel>  UpdateTrainer(int id, UpdateTrainerViewModel createTrainerViewModel);
        ViewResponse<TrainerViewModel>  DeleteById(int id);
    }
}
