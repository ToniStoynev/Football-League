using FootballLeague.Data;
using FootballLeague.DataAccess;
using FootballLeague.Services.Common;
using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services.Implementations;

public class TeamsService : ITeamsService
{
    private readonly FootballLeagueDbContext _dbContext;

    public TeamsService(FootballLeagueDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TeamDto>> AddTeam(TeamDto teamDto, CancellationToken cancellationToken)
    {
        if (teamDto is null)
        {
            return Result<TeamDto>.Failure(new List<string> { "Can not create team! teamDto is null" });
        }

        var team = _dbContext.Teams.Add(teamDto.Adapt<Team>());

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<TeamDto>.SuccessWith(team.Adapt<TeamDto>());
    }

    public async Task<TeamDto?> GetTeam(Guid teamId, CancellationToken cancellationToken)
    {
        var team = await _dbContext
            .Teams
            .SingleOrDefaultAsync(t => t.Id == teamId, cancellationToken);

        if (team is null)
        {
            return null;
        }

        return team.Adapt<TeamDto>();
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
             .OrderByDescending(t => t.Points)
             .ThenByDescending(t => t.ScoredGoals)
             .Select(t => t.Adapt<TeamDto>())
             .ToListAsync(cancellationToken);

        return ranking;
    }
}
