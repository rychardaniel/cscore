using Cscore.API.Models;

namespace Cscore.API.Services;

public interface IChampionshipService
{
    Task<List<ChampionshipModel>> GetAllAsync(int page, int pageSize);
    Task<ChampionshipModel?> GetByIdAsync(int id);
    Task<ChampionshipModel> CreateAsync(ChampionshipModel championship);
    Task<ChampionshipModel> UpdateAsync(int id, ChampionshipModel updatedData);
    Task DeleteAsync(int id);
}
