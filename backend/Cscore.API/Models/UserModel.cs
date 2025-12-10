using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cscore.API.Models;

public class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Role do usuário
    public RoleType Role { get; set; } = RoleType.Judge;

    // Associações como juiz
    [JsonIgnore]
    public List<ChampionshipJudgeModel>? ChampionshipJudges { get; set; }
}
