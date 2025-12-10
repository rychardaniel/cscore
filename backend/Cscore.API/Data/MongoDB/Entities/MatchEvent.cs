using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cscore.API.Data.MongoDB.Entities;

public class MatchEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // Referência à partida no PostgreSQL
    [BsonElement("match_id")]
    public int MatchId { get; set; }

    // Tipo de evento: goal, point, card, substitution, etc
    [BsonElement("event_type")]
    public string EventType { get; set; }

    // Momento do evento
    [BsonElement("occurred_at")]
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    // Minuto/tempo do jogo (se aplicável)
    [BsonElement("game_minute")]
    public int? GameMinute { get; set; }

    // ID do participante (referência ao PostgreSQL)
    [BsonElement("participant_id")]
    public int? ParticipantId { get; set; }

    // Detalhes adicionais do evento (estrutura varia)
    [BsonElement("details")]
    public BsonDocument? Details { get; set; }

    // Quem registrou o evento
    [BsonElement("registered_by_user_id")]
    public int RegisteredByUserId { get; set; }
}
