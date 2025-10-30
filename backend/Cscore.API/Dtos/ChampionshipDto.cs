namespace Cscore.API.Dtos;

public record CreateChampionshipDto(
    string Name,
    string University,
    DateTime StartDate,
    DateTime EndDate
);

public record ChampionshipResponseDto(
    string Id,
    string Name,
    string University,
    DateTime StartDate,
    DateTime EndDate
);