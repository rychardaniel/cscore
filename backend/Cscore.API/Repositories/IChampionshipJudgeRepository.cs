using Cscore.API.Models;

namespace Cscore.API.Repositories;

public interface IChampionshipJudgeRepository
{
    Task<List<ChampionshipJudgeModel>> GetByChampionshipIdAsync(int championshipId);
    Task<ChampionshipJudgeModel?> GetByChampionshipAndUserAsync(int championshipId, int userId);
    Task<bool> ExistsAsync(int championshipId, int userId);
    Task CreateAsync(ChampionshipJudgeModel championshipJudge);
    Task DeleteAsync(ChampionshipJudgeModel championshipJudge);
}
