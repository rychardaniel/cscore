using Cscore.API.Models;

namespace Cscore.API.Dtos;

public record CreateMatchDto(
    string Name,
    SportType SportType
);

public record MatchResponseDto(
    int Id,
    string Name,
    int ChampionshipId,
    SportType SportType
);