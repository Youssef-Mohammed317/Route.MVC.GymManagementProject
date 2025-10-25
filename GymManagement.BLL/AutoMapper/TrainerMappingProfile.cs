using AutoMapper;
using GymManagement.BLL.ViewModels.Member;
using GymManagement.BLL.ViewModels.Trainer;
using GymManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.BLL.AutoMapper
{
    public class TrainerMappingProfile : Profile
    {
        public TrainerMappingProfile()
        {
            CreateMap<Trainer, TrainerViewModel>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString()))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Adderss.BuildingNumber} - {src.Adderss.Street} - {src.Adderss.City}"))
                 .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialties.ToString()))
                 .ReverseMap();

            CreateMap<CreateTrainerViewModel, Trainer>()
                 .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialties))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                 .ForMember(dest => dest.Adderss, opt => opt.MapFrom(src => src))
                 .ForMember(dest => dest.Created_at, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();

            CreateMap<CreateTrainerViewModel, Address>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ReverseMap();

            CreateMap<UpdateTrainerViewModel, Trainer>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialties))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.Adderss, opt => opt.MapFrom(src => src))
                 .ForMember(dest => dest.Updated_at, opt => opt.MapFrom(src => DateTime.Now))
                .ReverseMap();
            CreateMap<UpdateTrainerViewModel, Address>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ReverseMap();

            CreateMap<Trainer,TrainerSelectModel>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
