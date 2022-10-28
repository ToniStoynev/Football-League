using FootballLeague.Data;
using FootballLeague.DataAccess;
using FootballLeague.Domain;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services.Implementations;

public class MatchesService : IMatchesService
{
    private const int WinPoints = 3;
    private const int DrawPoints = 1;
    private readonly FootballLeagueDbContext _dbContext;
    public MatchesService(FootballLeagueDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddMatch(MatchDto matchDto, CancellationToken cancellationToken)
    {
        if (matchDto is null)
        {
            return false;
        }

        var homeTeam = await _dbContext.Teams.FindAsync(matchDto.HomeTeamId);

        if (homeTeam is null)
        {
            return false;
        }

        var awayTeam = await _dbContext.Teams.FindAsync(matchDto.AwayTeamId);

        if (awayTeam is null)
        {
            return false;
        }

        UpdateTeamsStatistics(matchDto, homeTeam, awayTeam);

        var match = matchDto.Adapt<Match>();

        _dbContext.Matches.Add(match);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<MatchDto?> GetMatch(Guid matchId, CancellationToken cancellationToken)
    {
        var match = await _dbContext
            .Matches
            .SingleOrDefaultAsync(t => t.Id == matchId, cancellationToken);

        if (match is null)
        {
            return null;
        }

        return match.Adapt<MatchDto>();
    }

    public async Task<IEnumerable<MatchDto>> GetMatchesByTeamId(Guid teamId, CancellationToken cancellationToken)
    {
        var matches = await _dbContext
             .Matches
             .Where(m => m.HomeTeamId == teamId
                    || m.AwayTeamId == teamId)
             .Select(m => m.Adapt<MatchDto>())
             .ToListAsync(cancellationToken);

        return matches;
    }

    public async Task<IEnumerable<MatchDto>> GetAll(CancellationToken cancellationToken)
    {
        var matches = await _dbContext
            .Matches
            .Select(m => m.Adapt<MatchDto>())
            .ToListAsync(cancellationToken);

        return matches;
    }

    public async Task<bool> UpdateMatch(MatchDto matchDto, CancellationToken cancellationToken)
    {
        var match = await _dbContext
                .Matches
                .FindAsync(matchDto.Id);

        if (match is null)
        {
            return false;
        }

        match.HomeTeamGoals = matchDto.HomeTeamGoals;
        match.AwayTeamGoals = matchDto.AwayTeamGoals;

        _dbContext.Update(match);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteMatch(Guid id, CancellationToken cancellationToken)
    {
        var match = await _dbContext
                 .Matches
                 .Include(m => m.HomeTeam)
                 .Include(m => m.AwayTeam)
                 .SingleOrDefaultAsync(m => m.Id == id);

        if (match is null)
        {
            return false;
        }

        RemoveMatchStatisticsFromTeams(match);

        match.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static void UpdateTeamsStatistics(MatchDto matchDto, Team homeTeam, Team awayTeam)
    {
        homeTeam.MatchPlayed++;
        awayTeam.MatchPlayed++;

        if (matchDto.HomeTeamGoals > matchDto.AwayTeamGoals)
        {
            homeTeam.ScoredGoals = matchDto.HomeTeamGoals;
            homeTeam.Wins++;
            homeTeam.Points += WinPoints;

            awayTeam.ScoredGoals = matchDto.AwayTeamGoals;
            awayTeam.Losses++;
        }
        else if (matchDto.HomeTeamGoals < matchDto.AwayTeamGoals)
        {
            homeTeam.ScoredGoals += matchDto.HomeTeamGoals;
            homeTeam.Losses++;

            awayTeam.ScoredGoals += matchDto.AwayTeamGoals;
            awayTeam.Wins++;
            awayTeam.Points += 3;
        }
        else
        {
            homeTeam.ScoredGoals += matchDto.HomeTeamGoals;
            homeTeam.Draws++;
            homeTeam.Points += DrawPoints;

            awayTeam.ScoredGoals += matchDto.AwayTeamGoals;
            awayTeam.Draws++;
            awayTeam.Points += DrawPoints;
        }
    }

    private static void RemoveMatchStatisticsFromTeams(Match match)
    {
        match.HomeTeam.MatchPlayed--;
        match.AwayTeam.MatchPlayed--;

        if (match.HomeTeamGoals > match.AwayTeamGoals)
        {
            match.HomeTeam.Wins--;
            match.HomeTeam.Points -= WinPoints;
            match.HomeTeam.ScoredGoals -= match.HomeTeamGoals;

            match.AwayTeam.Losses--;
            match.AwayTeam.ScoredGoals -= match.AwayTeamGoals; ;
        }
        else if (match.HomeTeamGoals < match.AwayTeamGoals)
        {
            match.HomeTeam.Losses--;
            match.HomeTeam.ScoredGoals -= match.HomeTeamGoals;

            match.AwayTeam.Wins--;
            match.AwayTeam.Points -= WinPoints;
            match.AwayTeam.ScoredGoals -= match.AwayTeamGoals;
        }
        else
        {
            match.HomeTeam.Draws--;
            match.HomeTeam.Points -= DrawPoints;
            match.HomeTeam.ScoredGoals -= match.HomeTeamGoals;

            match.AwayTeam.Draws--;
            match.AwayTeam.Points -= DrawPoints;
            match.AwayTeam.ScoredGoals -= match.AwayTeamGoals;
        }
    }
}
