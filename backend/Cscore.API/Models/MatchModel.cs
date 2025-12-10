using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Models;

public class MatchModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ChampionshipId { get; set; }

    [JsonIgnore]
    public ChampionshipModel Championship { get; set; }

    // Tipo de esporte/jogo
    public SportType SportType { get; set; }

    // Status da partida
    public MatchStatus Status { get; set; }

    // Datas
    public DateTime ScheduledDate { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    // Local
    public string? Venue { get; set; }

    // Referência ao placar no MongoDB (ObjectId)
    public string? MongoScoreId { get; set; }

    // Relações (PostgreSQL)
    public List<MatchParticipantModel>? Participants { get; set; }

    // Observações
    public string? Notes { get; set; }
}

public enum MatchStatus
{
    Scheduled = 1,     // Agendada
    InProgress = 2,    // Em andamento
    Finished = 3,      // Finalizada
    Canceled = 4,      // Cancelada
    Postponed = 5      // Adiada
}