using MongoDB.Bson;

namespace Cscore.API.Dtos;

public record UpdateScoreDto(
    BsonDocument ScoreData
);

public record CreateMatchEventDto(
    string EventType,
    int? GameMinute,
    int? ParticipantId,
    BsonDocument? Details
);

public record CreateParticipantDto(
    int Type,  // ParticipantType enum value
    string Name,
    string Side,
    string? LogoUrl
);
