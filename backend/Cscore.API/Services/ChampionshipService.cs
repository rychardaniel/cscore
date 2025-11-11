using Cscore.API.Data;
using Cscore.API.Models;
using MongoDB.Driver;

namespace Cscore.API.Services;

public class ChampionshipService
{
    private readonly IMongoCollection<ChampionshipModel> _championships;

    public ChampionshipService(MongoContext context)
    {
        _championships = context.Championships;
    }

    public async Task<List<ChampionshipModel>> GetAllAsync() =>
        await _championships.Find(_ => true).ToListAsync();

    public async Task<ChampionshipModel?> GetByIdAsync(string id) =>
        await _championships.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<ChampionshipModel> CreateAsync(ChampionshipModel championship)
    {
        await _championships.InsertOneAsync(championship);
        return championship;
    }

    public async Task<ChampionshipModel> UpdateAsync(string id, ChampionshipModel championship)
    {
        await _championships.ReplaceOneAsync(c => c.Id == id, championship);
        championship.Id = id;
        return championship;
    }

    public async Task DeleteAsync(string id) =>
        await _championships.DeleteOneAsync(c => c.Id == id);
}