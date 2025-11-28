using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;

namespace Cscore.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, UserResponseDto>();
        CreateMap<RegisterUserDto, UserModel>();
        CreateMap<LoginUserDto, UserModel>();
    }
}
