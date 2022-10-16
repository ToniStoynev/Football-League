using FootballLeague.Services.Models;

namespace FootballLeague.Services.Contracts;

public interface IMatchesService
{
    Task<bool> AddMatch(MatchDto matchDto, CancellationToken cancellationToken);

    Task<MatchDto?> GetMatch(Guid matchId, CancellationToken cancellationToken);

    Task<IEnumerable<MatchDto>> GetMatchesByTeamId(Guid teamId, CancellationToken cancellationToken);

    Task<IEnumerable<MatchDto>> GetAll(CancellationToken cancellationToken);

    Task<bool> UpdateMatch(MatchDto matchDto, CancellationToken cancellationToken);

    Task<bool> DeleteMatch(Guid id, CancellationToken cancellationToken);
}
