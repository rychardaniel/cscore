using Cscore.API.Data.MongoDB.Entities;

namespace Cscore.API.Data.MongoDB.Repositories;

public interface IMatchScoreRepository
{
    Task<MatchScore?> GetByMatchIdAsync(int matchId);
    Task<MatchScore> CreateAsync(MatchScore matchScore);
    Task<MatchScore> UpdateAsync(MatchScore matchScore);
    Task DeleteAsync(string id);
}
