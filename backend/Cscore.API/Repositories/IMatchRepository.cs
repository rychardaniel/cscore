using Cscore.API.Models;

namespace Cscore.API.Repositories;

public interface IMatchRepository
{
    Task<List<MatchModel>> GetAllAsync(int championshipId, int page, int pageSize);
    Task<MatchModel?> GetByIdAsync(int championshipId, int id);
    Task CreateAsync(MatchModel match);
    Task UpdateAsync(MatchModel match);
    Task DeleteAsync(MatchModel match);
    
    // Public queries
    Task<List<MatchModel>> GetPublicMatchesAsync(int? championshipId, SportType? sportType, MatchStatus? status, DateTime? date, int page, int pageSize);
    Task<MatchModel?> GetByIdPublicAsync(int id);
    Task<List<MatchModel>> GetLiveMatchesAsync();
}
