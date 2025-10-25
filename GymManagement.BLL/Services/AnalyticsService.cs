using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Analytics;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ViewResponse<AnalyticsViewModel> GetAnalytics()
        {
            var analytics = new AnalyticsViewModel
            {
                TotalMembers = unitOfWork.MemberRepository.GetAll().Count(),
                ActiveMembers = unitOfWork.MembershipRepository.GetAll().ToList()
                .Count(m => m.Status == "Active"),
                TotalTrainers = unitOfWork.TrainerRepository.GetAll().Count(),
                UpcomingSessions = unitOfWork.SessionRepository.GetAll()
                    .Count(s => s.StartDate > DateTime.Now),
                OngoingSessions = unitOfWork.SessionRepository.GetAll()
                    .Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),
                CompletedSessions = unitOfWork.SessionRepository.GetAll()
                    .Count(s => s.EndDate < DateTime.Now)

            };

            return ViewResponse<AnalyticsViewModel>.Success(analytics);
        }
    }
}
