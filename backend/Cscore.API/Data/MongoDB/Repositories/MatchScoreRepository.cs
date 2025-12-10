using Cscore.API.Data.MongoDB.Entities;
using MongoDB.Driver;

namespace Cscore.API.Data.MongoDB.Repositories;

public class MatchScoreRepository : IMatchScoreRepository
{
    private readonly IMongoCollection<MatchScore> _collection;

    public MatchScoreRepository(MongoContext mongoContext)
    {
        _collection = mongoContext.MatchScores;
    }

    public async Task<MatchScore?> GetByMatchIdAsync(int matchId)
    {
        var filter = Builders<MatchScore>.Filter.Eq(s => s.MatchId, matchId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<MatchScore> CreateAsync(MatchScore matchScore)
    {
        await _collection.InsertOneAsync(matchScore);
        return matchScore;
    }

    public async Task<MatchScore> UpdateAsync(MatchScore matchScore)
    {
        var filter = Builders<MatchScore>.Filter.Eq(s => s.Id, matchScore.Id);
        await _collection.ReplaceOneAsync(filter, matchScore);
        return matchScore;
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<MatchScore>.Filter.Eq(s => s.Id, id);
        await _collection.DeleteOneAsync(filter);
    }
}
