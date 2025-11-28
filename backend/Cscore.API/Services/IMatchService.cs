using Cscore.API.Models;

namespace Cscore.API.Services;

public interface IMatchService
{
    Task<List<MatchModel>> GetAllAsync(int championshipId, int page, int pageSize);
    Task<MatchModel?> GetByIdAsync(int championshipId, int id);
    Task<MatchModel> CreateAsync(int championshipId, MatchModel match);
    Task<MatchModel> UpdateAsync(int championshipId, int id, MatchModel updatedData);
    Task DeleteAsync(int championshipId, int id);
}
