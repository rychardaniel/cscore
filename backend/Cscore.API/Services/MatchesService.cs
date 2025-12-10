using Cscore.API.Data;
using Cscore.API.Models;
using Cscore.API.Repositories;

namespace Cscore.API.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IChampionshipRepository _championshipRepository;
    private readonly AppDbContext _db;

    public MatchService(IMatchRepository matchRepository, IChampionshipRepository championshipRepository, AppDbContext db)
    {
        _matchRepository = matchRepository;
        _championshipRepository = championshipRepository;
        _db = db;
    }

    public async Task<List<MatchModel>> GetAllAsync(int championshipId, int page, int pageSize) =>
        await _matchRepository.GetAllAsync(championshipId, page, pageSize);

    public async Task<MatchModel?> GetByIdAsync(int championshipId, int id) =>
        await _matchRepository.GetByIdAsync(championshipId, id);

    public async Task<MatchModel> CreateAsync(int championshipId, MatchModel match)
    {
        var championship = await _championshipRepository.GetByIdAsync(championshipId);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        match.ChampionshipId = championshipId;

        await _matchRepository.CreateAsync(match);

        return match;
    }

    public async Task<MatchModel> UpdateAsync(int championshipId, int id, MatchModel updatedData)
    {
        var championship = await _championshipRepository.GetByIdAsync(championshipId);

        if (championship is null)
            throw new ArgumentException("Campeonato não encontrado");

        var match = await _matchRepository.GetByIdAsync(championshipId, id);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        match.ChampionshipId = championshipId;
        match.Name = updatedData.Name;
        match.SportType = updatedData.SportType;
        match.Status = updatedData.Status;
        match.ScheduledDate = updatedData.ScheduledDate;
        match.Venue = updatedData.Venue;
        match.Notes = updatedData.Notes;

        await _matchRepository.UpdateAsync(match);

        return match;
    }

    public async Task DeleteAsync(int championshipId, int id)
    {
        var match = await _matchRepository.GetByIdAsync(championshipId, id);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        await _matchRepository.DeleteAsync(match);
    }

    public async Task<List<MatchModel>> GetPublicMatches(
        int? championshipId,
        SportType? sportType,
        MatchStatus? status,
        DateTime? date,
        int page,
        int pageSize)
    {
        return await _matchRepository.GetPublicMatchesAsync(
            championshipId, sportType, status, date, page, pageSize);
    }

    public async Task<MatchModel?> GetPublicMatchById(int id)
    {
        return await _matchRepository.GetByIdPublicAsync(id);
    }

    public async Task<List<MatchModel>> GetLiveMatches()
    {
        return await _matchRepository.GetLiveMatchesAsync();
    }

    public async Task StartMatch(int matchId)
    {
        var match = await _matchRepository.GetByIdPublicAsync(matchId);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        if (match.Status != MatchStatus.Scheduled)
            throw new InvalidOperationException("Apenas partidas agendadas podem ser iniciadas");

        match.Status = MatchStatus.InProgress;
        match.StartedAt = DateTime.UtcNow;

        await _matchRepository.UpdateAsync(match);
    }

    public async Task FinishMatch(int matchId)
    {
        var match = await _matchRepository.GetByIdPublicAsync(matchId);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        if (match.Status != MatchStatus.InProgress)
            throw new InvalidOperationException("Apenas partidas em andamento podem ser finalizadas");

        match.Status = MatchStatus.Finished;
        match.FinishedAt = DateTime.UtcNow;

        await _matchRepository.UpdateAsync(match);
    }

    public async Task<MatchParticipantModel> AddParticipant(int matchId, MatchParticipantModel participant)
    {
        var match = await _matchRepository.GetByIdPublicAsync(matchId);

        if (match is null)
            throw new ArgumentException("Partida não encontrada");

        participant.MatchId = matchId;

        _db.MatchParticipants.Add(participant);
        await _db.SaveChangesAsync();

        return participant;
    }
}