using Cscore.API.Data;
using Cscore.API.Models;
using MongoDB.Driver;

namespace Cscore.API.Services;

public class MatchService
{
    private readonly IMongoCollection<MatchModel> _matches;
    private readonly ChampionshipService _championshipService;

    public MatchService(MongoContext context, ChampionshipService championshipService)
    {
        _matches = context.Matches;
        _championshipService = championshipService;
    }

    public async Task<List<MatchModel>> GetAllAsync(string idChampionship) =>
        await _matches.Find(x => x.IdChampionship == idChampionship).ToListAsync();

    public async Task<MatchModel?> GetByIdAsync(string idChampionship, string id) =>
        await _matches.Find(x => x.IdChampionship == idChampionship && x.Id == id).FirstOrDefaultAsync();

    public async Task<MatchModel> CreateAsync(string idChampionship, MatchModel match)
    {
        var championship = await _championshipService.GetByIdAsync(idChampionship);

        if (championship == null)
            throw new ArgumentException("Campeonato não encontrado");

        match.IdChampionship = idChampionship;
        
        await _matches.InsertOneAsync(match);
        return match;
    }

    public async Task<MatchModel> UpdateAsync(string idChampionship, string id, MatchModel match)
    {
        var championship = await _championshipService.GetByIdAsync(idChampionship);

        if (championship == null)
            throw new ArgumentException("Campeonato não encontrado");

        var matchExists = GetByIdAsync(idChampionship, id);

        if (matchExists.Result == null)
            throw new ArgumentException("Partida não encontrado");

        match.Id = id;
        match.IdChampionship = idChampionship;
        
        await _matches.ReplaceOneAsync(x => x.IdChampionship == idChampionship && x.Id == id, match);
        championship.Id = id;
        return match;
    }

    public async Task<MatchModel?> DeleteAsync(string idChampionship, string id) =>
        await _matches.FindOneAndDeleteAsync(x => x.IdChampionship == idChampionship && x.Id == id);
}