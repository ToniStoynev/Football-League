namespace FootballLeague.Services.Models;

public class MatchDto : BaseDto<Guid>
{
    public Guid HomeTeamId { get; set; }

    public Guid AwayTeamId { get; set; }

    public int HomeTeamGoals { get; set; }

    public int AwayTeamGoals { get; set; }
}
