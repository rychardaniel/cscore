using Cscore.API.Dtos;
using Cscore.API.Models;

namespace Cscore.API.Services;

public interface IUserService
{
    Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);
    Task<string> LoginAsync(LoginUserDto dto);
    Task<UserResponseDto?> GetByIdAsync(int id);
}

