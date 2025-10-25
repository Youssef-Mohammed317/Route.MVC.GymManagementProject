using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Plan;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.Services
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public ViewResponse<IEnumerable<PlanViewModel>> GetAllPlans()
        {
            var plans = unitOfWork.PlanRepository.GetAll()
                .ProjectTo<PlanViewModel>(mapper.ConfigurationProvider).ToList();
            return ViewResponse<IEnumerable<PlanViewModel>>.Success(plans);
        }

        public ViewResponse<PlanViewModel> GetPlanById(int id)
        {
            var planModel = unitOfWork.PlanRepository.GetById(id);
            if (planModel is not null)
            {
                return ViewResponse<PlanViewModel>.Success(mapper.Map<PlanViewModel>(planModel), "Plan found");
            }
            return ViewResponse<PlanViewModel>.Fail("Plan not found");
        }

        public ViewResponse<PlanViewModel> UpdatePlan(int id, UpdatePlanViewModel updateModel)
        {
            var planModel = unitOfWork.PlanRepository.GetById(id);

            if (planModel is not null)
            {
                var IsHasActiveMemberShips = HasActiveMemberships(planModel.PlanMembers);
                if (!IsHasActiveMemberShips)
                {
                    planModel = mapper.Map(updateModel, planModel);
                    unitOfWork.PlanRepository.Update(planModel);
                    if (unitOfWork.SaveChanges() > 0)
                    {

                        return ViewResponse<PlanViewModel>.Success(mapper.Map<PlanViewModel>(planModel), "Plan updated successfully");
                    }
                    return ViewResponse<PlanViewModel>.Fail("Failed to update plan");
                }
                return ViewResponse<PlanViewModel>.Fail("Cannot update plan with active memberships");
            }
            return ViewResponse<PlanViewModel>.Fail("Plan not found");
        }

        public ViewResponse<PlanViewModel> TogglePlan(int id)
        {
            var planModel = unitOfWork.PlanRepository.GetById(id);
            if (planModel is not null)
            {
                planModel.IsActive = !planModel.IsActive;

                planModel = unitOfWork.PlanRepository.Update(planModel);

                if (unitOfWork.SaveChanges() > 0)
                {
                    return ViewResponse<PlanViewModel>.Success(mapper.Map<PlanViewModel>(planModel), "Plan status toggled successfully");
                }
                return ViewResponse<PlanViewModel>.Fail("Failed to toggle plan status");
            }
            return ViewResponse<PlanViewModel>.Fail("Plan not found");
        }
        public ViewResponse<UpdatePlanViewModel> GetPlanByIdForUpdate(int id)
        {
            var planModel = unitOfWork.PlanRepository.GetById(id);
            if (planModel is not null)
            {
                var updatePlanViewModel = mapper.Map<UpdatePlanViewModel>(planModel);
                return ViewResponse<UpdatePlanViewModel>.Success(updatePlanViewModel, "Plan found");
            }
            return ViewResponse<UpdatePlanViewModel>.Fail("Plan not found");
        }
        private bool HasActiveMemberships(IEnumerable<Membership> memberships)
        {

            bool hasActiveMemberShips = false;

            foreach (var memberShip in memberships)
            {
                if (memberShip.Status == "Active")
                {
                    return true;
                }
            }


            return hasActiveMemberShips;
        }

    }
}
