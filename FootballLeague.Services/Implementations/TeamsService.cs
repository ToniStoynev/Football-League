using FootballLeague.Data;
using FootballLeague.DataAccess;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services.Implementations;

public class TeamsService : ITeamsService
{
    private readonly FootballLeagueDbContext _dbContext;

    public TeamsService(FootballLeagueDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddTeam(TeamDto teamDto, CancellationToken cancellationToken)
    {
        if (teamDto is null)
        {
            return false;
        }

        var team = new Team
        {
            Name = teamDto.Name
        };

        _dbContext.Teams.Add(team);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<TeamDto?> GetTeam(Guid teamId, CancellationToken cancellationToken)
    {
        var team = await _dbContext
            .Teams
            .SingleOrDefaultAsync(t => t.Id == teamId && !t.IsDeleted, cancellationToken);

        if (team is null)
        {
            return null;
        }

        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            MatchPlayed = team.MatchPlayed,
            Draws = team.Draws,
            Losses = team.Losses,
            Wins = team.Wins
        };
    }

    public async Task<bool> UpdateTeam(TeamDto teamDto, CancellationToken cancellationToken)
    {
        var team = await _dbContext
                .Teams
                .FindAsync(teamDto.Id);

        if (team is null)
        {
            return false;
        }

        team.Name = teamDto.Name;
        team.MatchPlayed = teamDto.MatchPlayed;
        team.Wins = teamDto.Wins;
        team.Draws = teamDto.Draws;
        team.Losses = teamDto.Losses;

        _dbContext.Update(team);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteTeam(Guid id, CancellationToken cancellationToken)
    {
        var team = await _dbContext
                 .Teams
                 .FindAsync(id);

        if (team is null)
        {
            return false;
        }

        team.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<TeamDto>> GetRanking(CancellationToken cancellationToken)
    {
        var ranking = await _dbContext
             .Teams
             .Where(t => !t.IsDeleted)
             .OrderByDescending(t => t.Points)
             .ThenByDescending(t => t.ScoredGoals)
             .Select(t => new TeamDto
             {
                 Id = t.Id,
                 Name = t.Name,
                 MatchPlayed = t.MatchPlayed,
                 Wins = t.Wins,
                 Draws = t.Draws,
                 Losses = t.Losses,
                 Points = t.Points,
                 ScoredGoals = t.ScoredGoals
             })
             .ToListAsync(cancellationToken);

        return ranking;
    }
}
