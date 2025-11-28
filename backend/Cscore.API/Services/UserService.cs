using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;
using Cscore.API.Repositories;
using Isopoh.Cryptography.Argon2;
using Microsoft.IdentityModel.Tokens;

namespace Cscore.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new ArgumentException("E-mail já cadastrado");

        var passwordHash = Argon2.Hash(dto.Password);

        var user = new UserModel
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = passwordHash
        };

        await _userRepository.CreateAsync(user);

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new ArgumentException("E-mail ou senha inválidos");

        if (!Argon2.Verify(user.PasswordHash, dto.Password))
            throw new ArgumentException("E-mail ou senha inválidos");

        return GenerateJwtToken(user);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserResponseDto>(user);
    }

    private string GenerateJwtToken(UserModel user)

    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]!);
        var tokenDescriptor = new SecurityTokenDescriptor


        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
