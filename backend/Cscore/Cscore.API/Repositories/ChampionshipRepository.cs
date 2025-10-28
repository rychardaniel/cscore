using Cscore.API.Data;
using Cscore.API.Models;
using MongoDB.Driver;

namespace Cscore.API.Repositories;

public class ChampionshipRepository
{
    private readonly IMongoCollection<ChampionshipModel> _championships;

    public ChampionshipRepository(MongoContext context)
    {
        _championships = context.Championships;
    }
    
    public async Task<List<ChampionshipModel>> GetAllAsync() =>
        await _championships.Find(_ => true).ToListAsync();

    public async Task<ChampionshipModel?> GetByIdAsync(string id) =>
        await _championships.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ChampionshipModel usuario) =>
        await _championships.InsertOneAsync(usuario);

    public async Task UpdateAsync(string id, ChampionshipModel usuario) =>
        await _championships.ReplaceOneAsync(u => u.Id == id, usuario);

    public async Task DeleteAsync(string id) =>
        await _championships.DeleteOneAsync(u => u.Id == id);

}