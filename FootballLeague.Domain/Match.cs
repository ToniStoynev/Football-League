using FootballLeague.Data;
using FootballLeague.Domain.Contracts;

namespace FootballLeague.Domain;

public class Match : BaseEntity<Guid>, IDeleatableEntity
{
    public Guid HomeTeamId { get; set; }

    public Team HomeTeam { get; set; } = default!;

    public Guid AwayTeamId { get; set; }

    public Team AwayTeam { get; set; } = default!;

    public int HomeTeamGoals { get; set; }

    public int AwayTeamGoals { get; set; }

    public bool IsDeleted { get; set; }
}
