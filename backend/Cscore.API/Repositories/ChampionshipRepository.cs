using Cscore.API.Data;
using Cscore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Repositories;

public class ChampionshipRepository : IChampionshipRepository
{
    private readonly AppDbContext _db;

    public ChampionshipRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ChampionshipModel>> GetAllAsync(int page, int pageSize)
    {
        return await _db.Championships
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<ChampionshipModel?> GetByIdAsync(int id)
    {
        return await _db.Championships.FindAsync(id);
    }

    public async Task CreateAsync(ChampionshipModel championship)
    {
        await _db.Championships.AddAsync(championship);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(ChampionshipModel championship)
    {
        _db.Championships.Update(championship);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChampionshipModel championship)
    {
        _db.Championships.Remove(championship);
        await _db.SaveChangesAsync();
    }
}