using FootballLeague.Domain;

namespace FootballLeague.Services.Specifications;

internal class GetMatchByIdSpecificaiton : Specification<Match>
{
    public GetMatchByIdSpecificaiton(Guid matchId)
        : base(match => match.Id == matchId)
    {

    }
}
