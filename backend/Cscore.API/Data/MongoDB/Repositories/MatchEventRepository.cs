using Cscore.API.Data.MongoDB.Entities;
using MongoDB.Driver;

namespace Cscore.API.Data.MongoDB.Repositories;

public class MatchEventRepository : IMatchEventRepository
{
    private readonly IMongoCollection<MatchEvent> _collection;

    public MatchEventRepository(MongoContext mongoContext)
    {
        _collection = mongoContext.MatchEvents;
    }

    public async Task<List<MatchEvent>> GetByMatchIdAsync(int matchId)
    {
        var filter = Builders<MatchEvent>.Filter.Eq(e => e.MatchId, matchId);
        return await _collection
            .Find(filter)
            .SortByDescending(e => e.OccurredAt)
            .ToListAsync();
    }

    public async Task<MatchEvent> CreateAsync(MatchEvent matchEvent)
    {
        await _collection.InsertOneAsync(matchEvent);
        return matchEvent;
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<MatchEvent>.Filter.Eq(e => e.Id, id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteAllByMatchIdAsync(int matchId)
    {
        var filter = Builders<MatchEvent>.Filter.Eq(e => e.MatchId, matchId);
        await _collection.DeleteManyAsync(filter);
    }
}
