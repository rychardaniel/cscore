namespace Cscore.API.DTOs;

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