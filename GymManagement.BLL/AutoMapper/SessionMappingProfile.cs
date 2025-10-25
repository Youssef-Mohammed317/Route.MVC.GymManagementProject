using AutoMapper;
using GymManagement.BLL.ViewModels.SessionViewModel;
using GymManagement.DAL.Entites;
using GymManagement.DAL.Repositories.Implementations;
using GymManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.AutoMapper
{
    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<Session, SessionViewModel>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SessionCategory.CategoryName))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descripcion))
               .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
               .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
               .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
               .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore())
               .ReverseMap();


            CreateMap<CreateSessionViewModel, Session>()
                 .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                 .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                 .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId))
                 .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                 .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                 .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.Created_at, opt => opt.MapFrom(src => DateTime.Now))
                 .ReverseMap();

            CreateMap<UpdateSessionViewModel, Session>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.TrainerId, opt => opt.MapFrom(src => src.TrainerId))
                 .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                 .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                 .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.Updated_at, opt => opt.MapFrom(src => DateTime.Now))
                 .ReverseMap();
        }
    }
}
