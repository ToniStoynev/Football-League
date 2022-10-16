namespace FootballLeague.Web.Models.RequestModels;

public class UpdateTeamRequest : CreateTeamRequest
{
    public int MatchPlayed { get; set; }

    public int Wins { get; set; }

    public int Draws { get; set; }

    public int Losses { get; set; }
}

