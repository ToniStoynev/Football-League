namespace FootballLeague.Services.Models;

public class TeamDto : BaseDto<Guid>
{
    public string Name { get; set; } = default!;

    public int MatchPlayed { get; set; }

    public int Wins { get; set; }

    public int Draws { get; set; }

    public int Losses { get; set; }

    public int Points { get; set; }

    public int ScoredGoals { get; set; }
}
