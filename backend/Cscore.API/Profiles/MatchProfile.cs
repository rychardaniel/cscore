using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;

namespace Cscore.API.Profiles;

public class MatchProfile: Profile
{
    public MatchProfile()
    {
        CreateMap<CreateMatchDto, MatchModel>();
        CreateMap<MatchModel, MatchResponseDto>();
    }
}