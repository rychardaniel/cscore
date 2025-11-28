using Cscore.API.Models;

namespace Cscore.API.Dtos;

public record CreateMatchDto(
    string Name,
    TypeMatch TypeMatch
);

public record MatchResponseDto(
    int Id,
    string Name,
    int ChampionshipId,
    TypeMatch TypeMatch
);