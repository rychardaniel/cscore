using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cscore.API.Models;

public class ChampionshipModel
{
    [BsonId]
    [BsonIgnoreIfNull]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; } = String.Empty;
    
    [BsonElement("university")]
    public string University { get; set; } = String.Empty;

    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
}