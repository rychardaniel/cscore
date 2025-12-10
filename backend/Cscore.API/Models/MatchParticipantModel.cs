using System.Text.Json.Serialization;

namespace Cscore.API.Models;

public class MatchParticipantModel
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    [JsonIgnore]
    public MatchModel Match { get; set; }

    // Tipo: Individual ou Time
    public ParticipantType Type { get; set; }

    // Nome do time ou jogador
    public string Name { get; set; }

    // Side: home, away, player1, player2, etc
    public string Side { get; set; }

    // Logo/avatar (opcional)
    public string? LogoUrl { get; set; }

    // Resultado (vencedor, perdedor, empate)
    public ParticipantResult? Result { get; set; }
}

public enum ParticipantType
{
    Individual = 1,
    Team = 2
}

public enum ParticipantResult
{
    Winner = 1,
    Loser = 2,
    Draw = 3
}
