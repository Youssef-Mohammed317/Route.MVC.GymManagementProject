using AutoMapper;
using GymManagement.BLL.ViewModels.MemberSession;
using GymManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagement.BLL.AutoMapper
{
    public class MemberSessionProfile : Profile
    {
        public MemberSessionProfile()
        {
            CreateMap<MemberSession, MemberSessionViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.IsAttended, opt => opt.MapFrom(src => src.IsAttended))
                .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.Created_at))
                .ReverseMap();


        }
    }
}
