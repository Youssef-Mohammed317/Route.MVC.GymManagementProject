using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Trainer;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Entites.Enums;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public ViewResponse<TrainerViewModel> CreateTrainer(CreateTrainerViewModel createModel)
        {
            var trainerModel = mapper.Map<Trainer>(createModel);

            unitOfWork.TrainerRepository.Create(trainerModel);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                    "Trainer created successfully");
            }

            return ViewResponse<TrainerViewModel>.Fail("Failed to create trainer");
        }

        public ViewResponse<TrainerViewModel> DeleteById(int id)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetById(id);

            if (trainerModel is not null)
            {
                if (!HasFutureSession(trainerModel))
                {
                    unitOfWork.TrainerRepository.Delete(trainerModel);

                    if (unitOfWork.SaveChanges() > 0)
                    {
                        return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                            "Trainer deleted successfully");
                    }
                }
                return ViewResponse<TrainerViewModel>.Fail("Trainer Has Future Sessions");
            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }
        private bool HasFutureSession(Trainer trainer)
        {

            foreach (var session in trainer.TrainerSessions)
            {
                if (session.StartDate > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        public ViewResponse<IEnumerable<TrainerViewModel>> GetAllTrainers()
        {
            var trainers = unitOfWork.TrainerRepository.GetAll()
                .ProjectTo<TrainerViewModel>(mapper.ConfigurationProvider);

            return ViewResponse<IEnumerable<TrainerViewModel>>.Success(trainers);
        }

        public ViewResponse<TrainerViewModel> GetTrainerByEmail(string email)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetByEmail(email);

            if (trainerModel != null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                    "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }

        public ViewResponse<TrainerViewModel> GetTrainerById(int id)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetById(id);
            if (trainerModel is not null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                   "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }

        public ViewResponse<TrainerViewModel> GetTrainerByPhone(string phone)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetByPhone(phone);
            if (trainerModel is not null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                        "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }
        public ViewResponse<TrainerViewModel> UpdateTrainer(int id, UpdateTrainerViewModel updateModel)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetById(id);

            if (trainerModel == null)
            {
                return ViewResponse<TrainerViewModel>.Fail("Trainer not found");
            }

            trainerModel = mapper.Map(updateModel, trainerModel);

            unitOfWork.TrainerRepository.Update(trainerModel);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainerModel),
                    "Trainer updated successfully");
            }

            return ViewResponse<TrainerViewModel>.Fail("Failed to update trainer");
        }

        public ViewResponse<UpdateTrainerViewModel> GetTrainerByIdForUpdate(int id)
        {
            var trainerModel = unitOfWork.TrainerRepository.GetById(id);

            if (trainerModel != null)
            {
                return ViewResponse<UpdateTrainerViewModel>.Success(
                    mapper.Map<UpdateTrainerViewModel>(trainerModel),
                    "Trainer Found");
            }
            return ViewResponse<UpdateTrainerViewModel>.Fail("Trainer Not Found");
        }


    }
}


