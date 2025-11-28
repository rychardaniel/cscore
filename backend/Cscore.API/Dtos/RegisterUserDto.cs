using System.ComponentModel.DataAnnotations;

namespace Cscore.API.Dtos;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
