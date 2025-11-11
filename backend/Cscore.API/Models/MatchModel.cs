using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cscore.API.Models;

public class MatchModel
{
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonRepresentation(BsonType.String)]
    public string? Id { get; set; } = Guid.NewGuid().ToString("N")[..8];

    [BsonElement("name")]
    public string Name { get; set; } = String.Empty;

    [BsonRepresentation(BsonType.String)]
    public string IdChampionship { get; set; } = String.Empty;
    
    [BsonElement("typeMatch")]
    public TypeMatch TypeMatch { get; set; }
}

public enum TypeMatch
{
    Futsal = 1,
    Volleyball = 2,
    Chess = 3
}