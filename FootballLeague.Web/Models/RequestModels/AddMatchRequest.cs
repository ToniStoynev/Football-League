namespace FootballLeague.Web.Models.RequestModels;

public class AddMatchRequest : ResultRequest
{
    public Guid HomeTeamId { get; set; }

    public Guid AwayTeamId { get; set; }
}
