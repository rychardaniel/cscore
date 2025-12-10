using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cscore.API.Data.MongoDB.Entities;

public class MatchScore
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // Referência à partida no PostgreSQL
    [BsonElement("match_id")]
    public int MatchId { get; set; }

    // Tipo de esporte (para validação)
    [BsonElement("sport_type")]
    public int SportType { get; set; }

    // Placar dinâmico (estrutura varia por esporte)
    [BsonElement("score_data")]
    public BsonDocument ScoreData { get; set; }

    // Última atualização
    [BsonElement("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Quem atualizou por último
    [BsonElement("updated_by_user_id")]
    public int UpdatedByUserId { get; set; }
}
