using System.Text.Json.Serialization;

namespace Cscore.API.Models;

public class ChampionshipJudgeModel
{
    public int Id { get; set; }

    public int ChampionshipId { get; set; }

    [JsonIgnore]
    public ChampionshipModel Championship { get; set; }

    public int UserId { get; set; }

    [JsonIgnore]
    public UserModel User { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
