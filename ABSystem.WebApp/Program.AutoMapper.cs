using ABSystem.Data.Models;
using ABSystem.Services.Dto;
using AutoMapper;

namespace ABSystem.WebApp
{
    public class Program : Profile
    {
        public Program()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Map Username
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper())) // Map Username
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Map Email
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper())); // Map NormalizedEmail;;

            CreateMap<User, UserDto>();

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Map Username
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper())) // Map Username
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Map Email
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper())); // Map NormalizedEmail;
        }        
    }
}
