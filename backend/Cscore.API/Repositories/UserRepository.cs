using Cscore.API.Data;
using Cscore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CreateAsync(UserModel user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }
}
