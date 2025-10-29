using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Category;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Session;
using GymManagement.BLL.ViewModels.Trainer;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Implementations;
using GymManagement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public ViewResponse<SessionViewModel> CreateSession(CreateSessionViewModel createModel)
        {
            var categoryModel = unitOfWork.CategoryRepository.GetById(createModel.CategoryId);
            var trainerModel = unitOfWork.TrainerRepository.GetById(createModel.TrainerId);

            if (trainerModel is not null && categoryModel is not null)
            {

                var sessionModel = mapper.Map<Session>(createModel);
                unitOfWork.SessionRepository.Create(sessionModel);
                if (unitOfWork.SaveChanges() > 0)
                {
                    var sessionViewModel = mapper.Map<SessionViewModel>(sessionModel);

                    sessionViewModel.AvailableSlots = GetAvailableSlots(sessionModel);

                    return ViewResponse<SessionViewModel>.Success(
                        sessionViewModel,
                        "Session created successfully.");
                }
                return ViewResponse<SessionViewModel>
                    .Fail("Failed to create session due to a database error.");
            }
            return ViewResponse<SessionViewModel>
                .Fail("Failed to create session. Invalid trainer or category.");
        }

        public ViewResponse<SessionViewModel> DeleteSessionById(int id)
        {
            var sessionModel = unitOfWork.SessionRepository.GetById(id);
            if (sessionModel != null)
            {
                unitOfWork.SessionRepository.Delete(sessionModel);
                if (unitOfWork.SaveChanges() > 0)
                {
                    var sessionViewModel = mapper.Map<SessionViewModel>(sessionModel);

                    sessionViewModel.AvailableSlots = GetAvailableSlots(sessionModel);

                    return ViewResponse<SessionViewModel>.Success(
                        sessionViewModel,
                        "Session deleted successfully.");
                }
                return ViewResponse<SessionViewModel>
                    .Fail("Failed to delete session due to a database error.");
            }

            return ViewResponse<SessionViewModel>
                .Fail("Session not found.");
        }

        public ViewResponse<IEnumerable<SessionViewModel>> GetAllSessions()
        {
            var sessions = unitOfWork.SessionRepository
                .GetAll()
                .Include(s => s.SessionTrainer)
                .Include(s => s.SessionCategory)
                .ProjectTo<SessionViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return ViewResponse<IEnumerable<SessionViewModel>>.Success(
                sessions,
                "Sessions retrieved successfully.");
        }

        public ViewResponse<CreateSessionViewModel> GetDataForCreateSession()
        {
            var createSessionModel = new CreateSessionViewModel();
            var categories = unitOfWork.CategoryRepository
                .GetAll()
                .ProjectTo<CategorySelectModel>(mapper.ConfigurationProvider)
                .ToList();
            var trainers = unitOfWork.TrainerRepository
                .GetAll()
                .ProjectTo<TrainerSelectModel>(mapper.ConfigurationProvider)
                .ToList();
            createSessionModel.Categories = categories;
            createSessionModel.Trainers = trainers;
            return ViewResponse<CreateSessionViewModel>.Success(
                createSessionModel,
                "Data for creating session retrieved successfully.");
        }

        public ViewResponse<SessionViewModel> GetSessionById(int id)
        {

            var sessionModel = unitOfWork.SessionRepository
                .GetByIdWithTrainerAndCategory(id);

            if (sessionModel != null)
            {
                var sessionViewModel = mapper.Map<SessionViewModel>(sessionModel);

                sessionViewModel.AvailableSlots = GetAvailableSlots(sessionModel);

                return ViewResponse<SessionViewModel>.Success(
                    sessionViewModel,
                    "Session found successfully.");
            }

            return ViewResponse<SessionViewModel>
                .Fail("Session not found.");
        }

        public ViewResponse<UpdateSessionViewModel> GetSessionByIdForUpdate(int id)
        {
            var sessionModel = unitOfWork.SessionRepository.GetById(id);
            if (sessionModel != null)
            {
                var updateModel = mapper.Map<UpdateSessionViewModel>(sessionModel);
                updateModel.Trainers = unitOfWork.TrainerRepository
                    .GetAll()
                    .ProjectTo<TrainerSelectModel>(mapper.ConfigurationProvider)
                    .ToList();
                return ViewResponse<UpdateSessionViewModel>.Success(updateModel,
                    "Session ready for update.");
            }
            return ViewResponse<UpdateSessionViewModel>
                .Fail("Session not found.");
        }

        public ViewResponse<SessionViewModel> UpdateSession(int id, UpdateSessionViewModel updateModel)
        {
            var sessionModel = unitOfWork.SessionRepository.GetById(id);
            var trainerModel = unitOfWork.TrainerRepository.GetById(updateModel.TrainerId);

            if (sessionModel == null || trainerModel == null)
            {
                return ViewResponse<SessionViewModel>
                    .Fail("Failed to update session. Invalid session, or trainer.");
            }

            sessionModel = mapper.Map(updateModel, sessionModel);

            unitOfWork.SessionRepository.Update(sessionModel);

            if (unitOfWork.SaveChanges() > 0)
            {
                var sessionViewModel = mapper.Map<SessionViewModel>(sessionModel);

                sessionViewModel.AvailableSlots = GetAvailableSlots(sessionModel);

                return ViewResponse<SessionViewModel>.Success(
                    sessionViewModel,
                    "Session created successfully.");
            }

            return ViewResponse<SessionViewModel>
                .Fail("Failed to update session due to a database error.");
        }

        private int GetAvailableSlots(Session sessionModel)
        {
            var bookedSlots = unitOfWork.MemberSessionRepository
                .GetAll()
                .Count(ms => ms.SessionId == sessionModel.Id);
            return sessionModel.Capacity - bookedSlots;
        }


    }

}
