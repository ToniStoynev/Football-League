using FootballLeague.Domain;
using FootballLeague.Domain.Contracts;

namespace FootballLeague.Data;

public class Team : BaseEntity<Guid>, IDeleatableEntity
{
    public Team()
    {
        Matches = new List<Match>();
    }

    public string Name { get; set; } = default!;

    public int MatchPlayed { get; set; }

    public int Wins { get; set; }

    public int Draws { get; set; }

    public int Losses { get; set; }

    public int Points { get; set; }

    public int ScoredGoals { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<Match> Matches { get; set; }
}
