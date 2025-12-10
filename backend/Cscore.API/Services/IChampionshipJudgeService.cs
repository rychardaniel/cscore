using Cscore.API.Models;

namespace Cscore.API.Services;

public interface IChampionshipJudgeService
{
    Task<List<ChampionshipJudgeModel>> GetJudgesByChampionshipAsync(int championshipId);
    Task<ChampionshipJudgeModel> AssignJudgeAsync(int championshipId, int userId);
    Task RemoveJudgeAsync(int championshipId, int userId);
}
