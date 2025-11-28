using Cscore.API.Data;
using Cscore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly AppDbContext _db;

    public MatchRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MatchModel>> GetAllAsync(int championshipId, int page, int pageSize)
    {
        return await _db.Matches
            .Where(m => m.ChampionshipId == championshipId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<MatchModel?> GetByIdAsync(int championshipId, int id)
    {
        return await _db.Matches
            .FirstOrDefaultAsync(m => m.ChampionshipId == championshipId && m.Id == id);
    }

    public async Task CreateAsync(MatchModel match)
    {
        await _db.Matches.AddAsync(match);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(MatchModel match)
    {
        _db.Matches.Update(match);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(MatchModel match)
    {
        _db.Matches.Remove(match);
        await _db.SaveChangesAsync();
    }
}
