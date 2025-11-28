using Cscore.API.Models;

namespace Cscore.API.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetByEmailAsync(string email);
    Task CreateAsync(UserModel user);
    Task<UserModel?> GetByIdAsync(int id);
}
