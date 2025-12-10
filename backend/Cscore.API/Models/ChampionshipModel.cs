using System.Text.Json.Serialization;

namespace Cscore.API.Models;

public class ChampionshipModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string University { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public List<MatchModel>? Matches { get; set; }

    // Juízes associados
    [JsonIgnore]
    public List<ChampionshipJudgeModel>? Judges { get; set; }
}