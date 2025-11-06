

using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.MemberSession;
using GymManagement.BLL.ViewModels.Session;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Interfaces;

namespace GymManagement.BLL.Services
{
    public class MemberSessionService : IMemberSessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberSessionService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public ViewResponse<MemberSessionViewModel> CreateMemberSession(int id, CreateMemberSessionViewModel createModel)
        {
            var memberSessionModel = new MemberSession
            {
                SessionId = id,
                MemberId = createModel.MemberId,
                Created_at = DateTime.Now
            };

            unitOfWork.MemberSessionRepository.Create(memberSessionModel);
            if (unitOfWork.SaveChanges() > 0)
            {
                var memberSession = unitOfWork.MemberSessionRepository.GetById(memberSessionModel.Id);
                return ViewResponse<MemberSessionViewModel>.Success(mapper.Map<MemberSessionViewModel>(memberSession), "Member session created successfully.");
            }
            return ViewResponse<MemberSessionViewModel>.Fail("Failed to create member session.");
        }

        public ViewResponse<MemberSessionViewModel> DeleteMemberSession(int id)
        {
            var memberSession = unitOfWork.MemberSessionRepository.GetById(id);
            if (memberSession == null)
            {
                return ViewResponse<MemberSessionViewModel>.Fail("Member session not found.");
            }
            unitOfWork.MemberSessionRepository.Delete(memberSession);
            if (unitOfWork.SaveChanges() > 0)
            {
                var memberSessionViewModel = mapper.Map<MemberSessionViewModel>(memberSession);
                return ViewResponse<MemberSessionViewModel>.Success(memberSessionViewModel, "Member session deleted successfully.");
            }
            return ViewResponse<MemberSessionViewModel>.Fail("Failed to delete member session.");

        }

        public ViewResponse<IEnumerable<MemberSelectModel>> GetMembers()
        {
            var members = unitOfWork.MemberRepository.GetAll()
                .ProjectTo<MemberSelectModel>(mapper.ConfigurationProvider)
                .ToList();

            return ViewResponse<IEnumerable<MemberSelectModel>>.Success(members);
        }

        public ViewResponse<IEnumerable<SessionViewModel>> GetMemberSessions()
        {
            var sessions = unitOfWork.SessionRepository.GetAll()
                .ProjectTo<SessionViewModel>(mapper.ConfigurationProvider);

            return ViewResponse<IEnumerable<SessionViewModel>>.Success(sessions);
        }

        public ViewResponse<IEnumerable<MemberSessionViewModel>> GetMembersForSessionsBySessionId(int id)
        {
            var session = unitOfWork.SessionRepository.GetById(id);

            if (session == null)
            {
                return ViewResponse<IEnumerable<MemberSessionViewModel>>.Fail("Session not found.");
            }

            var members = session.SessionMembers
                .Where(ms => ms.SessionId == id)
                .Select(ms => new MemberSessionViewModel
                {
                    Id = ms.Id,
                    MemberId = ms.MemberId,
                    MemberName = ms.Member.Name,
                    SessionId = ms.SessionId,
                    IsAttended = ms.IsAttended,
                    BookingDate = ms.Created_at,
                    Status = mapper.Map<SessionViewModel>(ms.Session).Status
                });

            return ViewResponse<IEnumerable<MemberSessionViewModel>>.Success(members);
        }

        public ViewResponse<MemberSessionViewModel> ToggleAttendance(int id)
        {
            //var session = unitOfWork.SessionRepository.GetById(id);
            var memberSession = unitOfWork.MemberSessionRepository.GetById(id);

            if (memberSession == null)
            {
                return ViewResponse<MemberSessionViewModel>.Fail("Member session not found.");
            }

            memberSession.IsAttended = !memberSession.IsAttended;

            unitOfWork.MemberSessionRepository.Update(memberSession);

            if (unitOfWork.SaveChanges() > 0)
            {
                var memberSessionViewModel = mapper.Map<MemberSessionViewModel>(memberSession);
                return ViewResponse<MemberSessionViewModel>.Success(memberSessionViewModel, "Attendance status toggled successfully.");
            }

            return ViewResponse<MemberSessionViewModel>.Fail("Failed to toggle attendance status.");
        }
    }
}
