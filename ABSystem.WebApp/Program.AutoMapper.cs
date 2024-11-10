using ABSystem.Data.Models;
using ABSystem.Services.Dto;
using AutoMapper;

namespace ABSystem.WebApp
{
    public class Program : Profile
    {
        public Program()
        {
            CreateMap<UserDto, User>();
        }        
    }
}
