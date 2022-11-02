using FootballLeague.Services.Common;
using FootballLeague.Services.Models;

namespace FootballLeague.Services.Contracts;

public interface ITeamsService
{
    Task<Result<TeamDto>> AddTeam(TeamDto teamDto, CancellationToken cancellationToken);

    Task<TeamDto?> GetTeam(Guid teamId, CancellationToken cancellationToken);

    Task<bool> UpdateTeam(TeamDto teamDto, CancellationToken cancellationToken);

    Task<bool> DeleteTeam(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<TeamDto>> GetRanking(CancellationToken cancellationToken);
}
