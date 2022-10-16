using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Web.Models.RequestModels;

public class CreateTeamRequest
{
    private const string InvalidTeamNameMessage = "Team name should be between 3 and 20 characters";

    [StringLength(maximumLength: 20,
        ErrorMessage = InvalidTeamNameMessage,
        MinimumLength = 3)]
    public string Name { get; set; } = default!;
}
