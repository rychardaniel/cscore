using Cscore.API.Data;
using Cscore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Repositories;

public class ChampionshipJudgeRepository : IChampionshipJudgeRepository
{
    private readonly AppDbContext _db;

    public ChampionshipJudgeRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ChampionshipJudgeModel>> GetByChampionshipIdAsync(int championshipId)
    {
        return await _db.ChampionshipJudges
            .Include(cj => cj.User)
            .Where(cj => cj.ChampionshipId == championshipId)
            .ToListAsync();
    }

    public async Task<ChampionshipJudgeModel?> GetByChampionshipAndUserAsync(int championshipId, int userId)
    {
        return await _db.ChampionshipJudges
            .FirstOrDefaultAsync(cj => cj.ChampionshipId == championshipId && cj.UserId == userId);
    }

    public async Task<bool> ExistsAsync(int championshipId, int userId)
    {
        return await _db.ChampionshipJudges
            .AnyAsync(cj => cj.ChampionshipId == championshipId && cj.UserId == userId);
    }

    public async Task CreateAsync(ChampionshipJudgeModel championshipJudge)
    {
        await _db.ChampionshipJudges.AddAsync(championshipJudge);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChampionshipJudgeModel championshipJudge)
    {
        _db.ChampionshipJudges.Remove(championshipJudge);
        await _db.SaveChangesAsync();
    }
}
