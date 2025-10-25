using AutoMapper;
using GymManagement.BLL.ViewModels.Membership;
using GymManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.AutoMapper
{
    public class MembershipMappingProfile : Profile
    {
        public MembershipMappingProfile()
        {
            CreateMap<Membership, MembershipViewModel>();
            CreateMap<CreateMembershipViewModel, Membership>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.Created_at, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.EndDate, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
