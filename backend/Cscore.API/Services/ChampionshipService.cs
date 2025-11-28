using Cscore.API.Models;
using Cscore.API.Repositories;

namespace Cscore.API.Services;

public class ChampionshipService : IChampionshipService
{
    private readonly IChampionshipRepository _repository;

    public ChampionshipService(IChampionshipRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ChampionshipModel>> GetAllAsync(int page, int pageSize) =>
        await _repository.GetAllAsync(page, pageSize);

    public async Task<ChampionshipModel?> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<ChampionshipModel> CreateAsync(ChampionshipModel championship)
    {
        await _repository.CreateAsync(championship);
        return championship;
    }

    public async Task<ChampionshipModel> UpdateAsync(int id, ChampionshipModel updatedData)
    {
        var championship = await _repository.GetByIdAsync(id);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        championship.Name = updatedData.Name;
        championship.University = updatedData.University;
        championship.StartDate = updatedData.StartDate;
        championship.EndDate = updatedData.EndDate;

        await _repository.UpdateAsync(championship);
        return championship;
    }

    public async Task DeleteAsync(int id)
    {
        var championship = await _repository.GetByIdAsync(id);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        await _repository.DeleteAsync(championship);
    }
}