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

    public async Task<List<MatchModel>> GetPublicMatchesAsync(
        int? championshipId,
        SportType? sportType,
        MatchStatus? status,
        DateTime? date,
        int page,
        int pageSize)
    {
        var query = _db.Matches
            .Include(m => m.Championship)
            .Include(m => m.Participants)
            .AsQueryable();

        if (championshipId.HasValue)
            query = query.Where(m => m.ChampionshipId == championshipId.Value);

        if (sportType.HasValue)
            query = query.Where(m => m.SportType == sportType.Value);

        if (status.HasValue)
            query = query.Where(m => m.Status == status.Value);

        if (date.HasValue)
            query = query.Where(m => m.ScheduledDate.Date == date.Value.Date);

        return await query
            .OrderByDescending(m => m.ScheduledDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<MatchModel?> GetByIdPublicAsync(int id)
    {
        return await _db.Matches
            .Include(m => m.Championship)
            .Include(m => m.Participants)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<MatchModel>> GetLiveMatchesAsync()
    {
        return await _db.Matches
            .Include(m => m.Championship)
            .Include(m => m.Participants)
            .Where(m => m.Status == MatchStatus.InProgress)
            .OrderBy(m => m.StartedAt)
            .ToListAsync();
    }
}
