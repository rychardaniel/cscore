namespace Cscore.API.Dtos;

public record CreateChampionshipDto(
    string Name,
    DateTime StartDate,
    DateTime EndDate
);

public record ChampionshipResponseDto(
    string Id,
    string Name,
    DateTime StartDate,
    DateTime EndDate
);