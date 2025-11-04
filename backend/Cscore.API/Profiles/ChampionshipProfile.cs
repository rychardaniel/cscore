using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;

namespace Cscore.API.Profiles;

public class ChampionshipProfile: Profile
{
    public ChampionshipProfile()
    {
        CreateMap<CreateChampionshipDto, ChampionshipModel>();
        CreateMap<ChampionshipModel, ChampionshipResponseDto>();
    }
}