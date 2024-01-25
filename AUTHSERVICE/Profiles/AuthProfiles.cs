using AUTHSERVICE.Models.Dtos;
using AUTHSERVICE.Models;
using AutoMapper;

namespace AUTHSERVICE.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterRequestDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(r => r.Email));
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
        }
    }
}
