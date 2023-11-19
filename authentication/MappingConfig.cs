using authentication.Models;
using authentication.Models.Dto;
using AutoMapper;

namespace authentication;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
    
}