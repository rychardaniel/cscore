using Cscore.API.Models;
using Cscore.API.Repositories;

namespace Cscore.API.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IChampionshipRepository _championshipRepository;

    public MatchService(IMatchRepository matchRepository, IChampionshipRepository championshipRepository)
    {
        _matchRepository = matchRepository;
        _championshipRepository = championshipRepository;
    }

    public async Task<List<MatchModel>> GetAllAsync(int championshipId, int page, int pageSize) =>
        await _matchRepository.GetAllAsync(championshipId, page, pageSize);

    public async Task<MatchModel?> GetByIdAsync(int championshipId, int id) =>
        await _matchRepository.GetByIdAsync(championshipId, id);
    
    public async Task<MatchModel> CreateAsync(int championshipId, MatchModel match)
    {
        var championship = await _championshipRepository.GetByIdAsync(championshipId);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        match.ChampionshipId = championshipId;

        await _matchRepository.CreateAsync(match);

        return match;
    }

    public async Task<MatchModel> UpdateAsync(int championshipId, int id, MatchModel updatedData)
    {
        var championship = await _championshipRepository.GetByIdAsync(championshipId);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        var match = await _matchRepository.GetByIdAsync(championshipId, id);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        match.ChampionshipId = championshipId;
        match.Name = updatedData.Name;
        match.TypeMatch = updatedData.TypeMatch;

        await _matchRepository.UpdateAsync(match);

        return match;
    }

    public async Task DeleteAsync(int championshipId, int id) {
        var match = await _matchRepository.GetByIdAsync(championshipId, id);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        await _matchRepository.DeleteAsync(match);
    }
}