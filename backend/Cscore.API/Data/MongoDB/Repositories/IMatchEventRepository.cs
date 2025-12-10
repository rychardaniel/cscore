using Cscore.API.Data.MongoDB.Entities;

namespace Cscore.API.Data.MongoDB.Repositories;

public interface IMatchEventRepository
{
    Task<List<MatchEvent>> GetByMatchIdAsync(int matchId);
    Task<MatchEvent> CreateAsync(MatchEvent matchEvent);
    Task DeleteAsync(string id);
    Task DeleteAllByMatchIdAsync(int matchId);
}
