using AutoMapper;
using AutoMapper.QueryableExtensions;
using GymManagement.BLL.Interfaces;
using GymManagement.BLL.ViewModels.Common;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Membership;
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
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MembershipService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public ViewResponse<MembershipViewModel> CreateMembership(CreateMembershipViewModel createModel)
        {
            var membershipModel = mapper.Map<Membership>(createModel);
            var plan = unitOfWork.PlanRepository.GetById(createModel.PlanId);


            if (plan == null)
            {
                return ViewResponse<MembershipViewModel>.Fail("Invalid Plan Id.");
            }



            membershipModel.EndDate = DateTime.Now.AddDays(plan.DurationInDays);

            unitOfWork.MembershipRepository.Create(membershipModel);

            if (unitOfWork.SaveChanges() > 0)
            {
                var membershipViewModel = mapper.Map<MembershipViewModel>(membershipModel);
                return ViewResponse<MembershipViewModel>.Success(membershipViewModel, "Membership created successfully.");
            }
            return ViewResponse<MembershipViewModel>.Fail("Failed to create membership.");
        }

        public ViewResponse<IEnumerable<MembershipViewModel>> GetAllMemberships()
        {
            var memberships = unitOfWork.MembershipRepository.GetAll();

            return ViewResponse<IEnumerable<MembershipViewModel>>
                .Success(mapper.Map<IEnumerable<MembershipViewModel>>(memberships));
        }

        public ViewResponse<CreateMembershipViewModel> GetDataForCreateMembership()
        {

            var model = new CreateMembershipViewModel()
            {
                Members = unitOfWork.MemberRepository.GetAll()
                   .ProjectTo<MemberSelectModel>(mapper.ConfigurationProvider),
                Plans = unitOfWork.PlanRepository.GetAll()
                    .ProjectTo<PlanSelectModel>(mapper.ConfigurationProvider)
            };
            return ViewResponse<CreateMembershipViewModel>.Success(model);
        }
    }
}
