using Cscore.API.Models;

namespace Cscore.API.Dtos;

public record CreateMatchDto(
    string Name,
    TypeMatch TypeMatch
);

public record MatchResponseDto(
    string Id,
    string Name,
    string IdChampionship,
    TypeMatch TypeMatch
);