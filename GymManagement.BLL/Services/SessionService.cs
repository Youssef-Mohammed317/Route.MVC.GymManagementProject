using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.SessionViewModel;
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

        public ViewResponse<SessionViewModel> CreateSession(CreateSessionViewModel model)
        {
            var cat = unitOfWork.CategoryRepository.GetById(model.CategoryId);
            var trainer = unitOfWork.TrainerRepository.GetById(model.TrainerId);

            if (trainer is not null && cat is not null)
            {

                var session = mapper.Map<Session>(model);
                unitOfWork.SessionRepository.Create(session);
                if (unitOfWork.SaveChanges() > 0)
                {
                    var sessionViewModel = mapper.Map<SessionViewModel>(session);

                    sessionViewModel.AvailableSlots = GetAvailableSlots(session);

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
            var session = unitOfWork.SessionRepository.GetById(id);
            if (session != null)
            {
                unitOfWork.SessionRepository.Delete(session);
                if (unitOfWork.SaveChanges() > 0)
                {
                    var sessionViewModel = mapper.Map<SessionViewModel>(session);

                    sessionViewModel.AvailableSlots = GetAvailableSlots(session);

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
        public ViewResponse<SessionViewModel> GetSessionById(int id)
        {

            var session = unitOfWork.SessionRepository
                .GetByIdWithTrainerAndCategory(id);

            if (session != null)
            {
                var sessionViewModel = mapper.Map<SessionViewModel>(session);

                sessionViewModel.AvailableSlots = GetAvailableSlots(session);

                return ViewResponse<SessionViewModel>.Success(
                    sessionViewModel,
                    "Session found successfully.");
            }

            return ViewResponse<SessionViewModel>
                .Fail("Session not found.");
        }

        public ViewResponse<UpdateSessionViewModel> GetSessionByIdForUpdate(int id)
        {
            var session = unitOfWork.SessionRepository.GetById(id);
            if (session != null)
            {

                return ViewResponse<UpdateSessionViewModel>.Success(
                    mapper.Map<UpdateSessionViewModel>(session),
                    "Session created successfully.");
            }
            return ViewResponse<UpdateSessionViewModel>
                .Fail("Session not found.");
        }

        public ViewResponse<SessionViewModel> UpdateSession(int id, UpdateSessionViewModel model)
        {
            var sessionModel = unitOfWork.SessionRepository.GetById(id);
            var trainer = unitOfWork.TrainerRepository.GetById(model.TrainerId);

            if (sessionModel == null || trainer == null)
            {
                return ViewResponse<SessionViewModel>
                    .Fail("Failed to update session. Invalid session, or trainer.");
            }

            sessionModel = mapper.Map(model, sessionModel);

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

        private int GetAvailableSlots(Session session)
        {
            var bookedSlots = unitOfWork.MemberSessionRepository
                .GetAll()
                .Count(ms => ms.SessionId == session.Id);
            return session.Capacity - bookedSlots;
        }


    }

}
