using FootballLeague.Domain;

namespace FootballLeague.Services.Specifications;

internal class GetMatchesByTeamIdSpecification : Specification<Match>
{
    public GetMatchesByTeamIdSpecification(Guid teamId)
        : base(match => match.HomeTeamId == teamId || match.AwayTeamId == teamId)
    {
    }
}
