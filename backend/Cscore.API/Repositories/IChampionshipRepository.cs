using Cscore.API.Models;

namespace Cscore.API.Repositories;

public interface IChampionshipRepository
{
    Task<List<ChampionshipModel>> GetAllAsync(int page, int pageSize);
    Task<ChampionshipModel?> GetByIdAsync(int id);
    Task CreateAsync(ChampionshipModel championship);
    Task UpdateAsync(ChampionshipModel championship);
    Task DeleteAsync(ChampionshipModel championship);
}
