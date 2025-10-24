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

        public ViewResponse<TrainerViewModel> CreateTrainer(CreateTrainerViewModel createTrainerViewModel)
        {
            var trainer = mapper.Map<Trainer>(createTrainerViewModel);

            unitOfWork.TrainerRepository.Create(trainer);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
                    "Trainer created successfully");
            }

            return ViewResponse<TrainerViewModel>.Fail("Failed to create trainer");
        }

        public ViewResponse<TrainerViewModel> DeleteById(int id)
        {
            var trainer = unitOfWork.TrainerRepository.GetById(id);

            if (trainer is not null)
            {
                if (!HasFutureSession(trainer))
                {
                    unitOfWork.TrainerRepository.Delete(trainer);

                    if (unitOfWork.SaveChanges() > 0)
                    {
                        return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
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

        public ViewResponse<TrainerViewModel> GetByEmail(string email)
        {
            var trainer = unitOfWork.TrainerRepository.GetByEmail(email);

            if (trainer != null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
                    "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }

        public ViewResponse<TrainerViewModel> GetById(int id)
        {
            var trainer = unitOfWork.TrainerRepository.GetById(id);
            if (trainer is not null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
                   "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }

        public ViewResponse<TrainerViewModel> GetByPhone(string phone)
        {
            var trainer = unitOfWork.TrainerRepository.GetByPhone(phone);
            if (trainer is not null)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
                        "Trainer Found");

            }
            return ViewResponse<TrainerViewModel>.Fail("Trainer Not Found");
        }
        public ViewResponse<TrainerViewModel> UpdateTrainer(int id, UpdateTrainerViewModel updateTrainerViewModel)
        {
            var trainer = unitOfWork.TrainerRepository.GetById(id);

            if (trainer == null)
            {
                return ViewResponse<TrainerViewModel>.Fail("Trainer not found");
            }

            trainer = mapper.Map(updateTrainerViewModel, trainer);

            unitOfWork.TrainerRepository.Update(trainer);

            if (unitOfWork.SaveChanges() > 0)
            {
                return ViewResponse<TrainerViewModel>.Success(mapper.Map<TrainerViewModel>(trainer),
                    "Trainer updated successfully");
            }

            return ViewResponse<TrainerViewModel>.Fail("Failed to update trainer");
        }

        public ViewResponse<UpdateTrainerViewModel> GetTrainerByIdForUpdate(int id)
        {
            var trainer = unitOfWork.TrainerRepository.GetById(id);

            if (trainer != null)
            {
                return ViewResponse<UpdateTrainerViewModel>.Success(
                    mapper.Map<UpdateTrainerViewModel>(trainer),
                    "Trainer Found");
            }
            return ViewResponse<UpdateTrainerViewModel>.Fail("Trainer Not Found");
        }


    }
}


