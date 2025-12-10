using Cscore.API.Data.MongoDB.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cscore.API.Data;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public IMongoCollection<MatchScore> MatchScores { get; }
    public IMongoCollection<MatchEvent> MatchEvents { get; }

    public MongoContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);

        MatchScores = _database.GetCollection<MatchScore>("match_scores");
        MatchEvents = _database.GetCollection<MatchEvent>("match_events");

        // Criar índices
        CreateIndexes();
    }

    private void CreateIndexes()
    {
        // Índice único em match_id para match_scores
        var scoreIndexBuilder = Builders<MatchScore>.IndexKeys;
        var scoreIndexModel = new CreateIndexModel<MatchScore>(
            scoreIndexBuilder.Ascending(s => s.MatchId),
            new CreateIndexOptions { Unique = true }
        );
        
        try
        {
            MatchScores.Indexes.CreateOne(scoreIndexModel);
        }
        catch (MongoCommandException)
        {
            // Índice já existe
        }

        // Índice em match_id para match_events
        var eventIndexBuilder = Builders<MatchEvent>.IndexKeys;
        var eventIndexModel = new CreateIndexModel<MatchEvent>(
            eventIndexBuilder.Ascending(e => e.MatchId)
        );
        
        try
        {
            MatchEvents.Indexes.CreateOne(eventIndexModel);
        }
        catch (MongoCommandException)
        {
            // Índice já existe
        }
    }
}