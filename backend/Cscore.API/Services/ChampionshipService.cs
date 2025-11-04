using Cscore.API.Models;
using Cscore.API.Repositories;

namespace Cscore.API.Services;

public class ChampionshipService
{
    private readonly ChampionshipRepository _repository;

    public ChampionshipService(ChampionshipRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ChampionshipModel>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<ChampionshipModel?> GetByIdAsync(string id) =>
        await _repository.GetByIdAsync(id);

    public async Task<ChampionshipModel> CreateAsync(ChampionshipModel championship)
    {
        await _repository.CreateAsync(championship);
        return championship;
    }

    public async Task<ChampionshipModel> UpdateAsync(string id, ChampionshipModel championship)
    {
        await _repository.UpdateAsync(id, championship);
        championship.Id = id;
        return championship;
    }

    public async Task DeleteAsync(string id) =>
        await _repository.DeleteAsync(id);
}