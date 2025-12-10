using Cscore.API.Models;
using Cscore.API.Repositories;

namespace Cscore.API.Services;

public class ChampionshipJudgeService : IChampionshipJudgeService
{
    private readonly IChampionshipJudgeRepository _championshipJudgeRepo;
    private readonly IChampionshipRepository _championshipRepo;
    private readonly IUserRepository _userRepo;

    public ChampionshipJudgeService(
        IChampionshipJudgeRepository championshipJudgeRepo,
        IChampionshipRepository championshipRepo,
        IUserRepository userRepo)
    {
        _championshipJudgeRepo = championshipJudgeRepo;
        _championshipRepo = championshipRepo;
        _userRepo = userRepo;
    }

    public async Task<List<ChampionshipJudgeModel>> GetJudgesByChampionshipAsync(int championshipId)
    {
        return await _championshipJudgeRepo.GetByChampionshipIdAsync(championshipId);
    }

    public async Task<ChampionshipJudgeModel> AssignJudgeAsync(int championshipId, int userId)
    {
        // Verificar se campeonato existe
        var championship = await _championshipRepo.GetByIdAsync(championshipId);
        if (championship == null)
            throw new ArgumentException("Campeonato não encontrado");

        // Verificar se usuário existe
        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("Usuário não encontrado");

        // Verificar se já está associado
        var exists = await _championshipJudgeRepo.ExistsAsync(championshipId, userId);
        if (exists)
            throw new InvalidOperationException("Juiz já está associado a este campeonato");

        var championshipJudge = new ChampionshipJudgeModel
        {
            ChampionshipId = championshipId,
            UserId = userId
        };

        await _championshipJudgeRepo.CreateAsync(championshipJudge);

        return championshipJudge;
    }

    public async Task RemoveJudgeAsync(int championshipId, int userId)
    {
        var championshipJudge = await _championshipJudgeRepo.GetByChampionshipAndUserAsync(championshipId, userId);

        if (championshipJudge == null)
            throw new ArgumentException("Associação não encontrada");

        await _championshipJudgeRepo.DeleteAsync(championshipJudge);
    }
}
