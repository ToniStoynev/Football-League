using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using FootballLeague.Web.Models.RequestModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ITeamsService _teamsService;

    public TeamsController(ITeamsService teamsService)
    {
        _teamsService = teamsService;
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(Guid id, CancellationToken cancellationToken)
    {
        var team = await _teamsService.GetTeam(id, cancellationToken);

        if (team is null)
        {
            return NotFound($"Team with {id} does not exist");
        }

        return Ok(team);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(CreateTeamRequest), 201)]
    [HttpPost]
    public async Task<IActionResult> AddTeam(CreateTeamRequest createTeamRequest,
        CancellationToken cancellationToken)
    {
        var result = await _teamsService.AddTeam(
            new TeamDto() { Name = createTeamRequest.Name },
            cancellationToken);

        if (!result.Succeeded)
        {
            return BadRequest("Team was not created!");
        }

        return CreatedAtAction(nameof(GetTeam), new { id = result.Data.Id }, createTeamRequest);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam([FromRoute] Guid id,
        [FromBody] UpdateTeamRequest updateTeamRequest,
        CancellationToken cancellationToken)
    {
        var teamDto = updateTeamRequest.Adapt<TeamDto>();
        teamDto.Id = id;

        var isSuccessfullyUpdated = await _teamsService
            .UpdateTeam(teamDto,
                cancellationToken);

        if (!isSuccessfullyUpdated)
        {
            return NotFound($"Team with {id} does not exist");
        }

        return Ok(updateTeamRequest);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var isSuccessfullyDeleted = await _teamsService
            .DeleteTeam(id, cancellationToken);

        if (!isSuccessfullyDeleted)
        {
            return NotFound($"Team with {id} does not exist");
        }

        return NoContent();
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<IActionResult> GetRanking(CancellationToken cancellationToken)
    {
        var ranking = await _teamsService.GetRanking(cancellationToken);

        return Ok(ranking);
    }
}
