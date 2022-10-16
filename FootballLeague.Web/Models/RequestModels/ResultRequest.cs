using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Web.Models.RequestModels;

public class ResultRequest
{
    private const string InvalidGoalsNumberMessage = "There cannot be less than 0 or more than 30 goals in a match";

    [Range(0, 30, ErrorMessage = InvalidGoalsNumberMessage)]
    public int HomeTeamGoals { get; set; }

    [Range(0, 30, ErrorMessage = InvalidGoalsNumberMessage)]
    public int AwayTeamGoals { get; set; }
}
