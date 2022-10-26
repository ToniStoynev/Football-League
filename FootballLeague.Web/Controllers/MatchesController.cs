using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using FootballLeague.Web.Models.RequestModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatchesController : ControllerBase
{
    private readonly IMatchesService _matchesService;

    public MatchesController(IMatchesService matchesService)
    {
        _matchesService = matchesService;
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMatch(Guid id, CancellationToken cancellationToken)
    {
        var match = await _matchesService.GetMatch(id, cancellationToken);

        if (match is null)
        {
            return NotFound($"Match with {id} does not exist");
        }

        return Ok(match);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<IActionResult> GetAllMatch(CancellationToken cancellationToken)
    {
        var matches = await _matchesService.GetAll(cancellationToken);

        if (!matches.Any())
        {
            return NotFound("No matches played yet");
        }

        return Ok(matches);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpGet("teamId")]
    public async Task<IActionResult> GetAllMatchesByTeam(Guid teamId, CancellationToken cancellationToken)
    {
        var matches = await _matchesService.GetMatchesByTeamId(teamId, cancellationToken);

        if (!matches.Any())
        {
            return NotFound($"Team with id: {teamId} matches played yet");
        }

        return Ok(matches);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(AddMatchRequest), 201)]
    [HttpPost]
    public async Task<IActionResult> AddMatch(AddMatchRequest addMatchRequest,
        CancellationToken cancellationToken)
    {
        var matchDto = addMatchRequest.Adapt<MatchDto>();

        var isSuccessfullyAdded = await _matchesService.AddMatch(matchDto, cancellationToken);

        if (!isSuccessfullyAdded)
        {
            return BadRequest("Team passed do not exist!");
        }

        return CreatedAtAction(nameof(AddMatch), addMatchRequest);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMatch([FromRoute] Guid id,
        [FromBody] ResultRequest resultRequest,
        CancellationToken cancellationToken)
    {
        var matchDto = resultRequest.Adapt<MatchDto>();
        matchDto.Id = id;

        var isSuccessfullyUpdated = await _matchesService
            .UpdateMatch(matchDto, cancellationToken);

        if (!isSuccessfullyUpdated)
        {
            return NotFound($"Match with {id} does not exist");
        }

        return Ok(matchDto);
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMatch([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var isSuccessfullyDeleted = await _matchesService
            .DeleteMatch(id, cancellationToken);

        if (!isSuccessfullyDeleted)
        {
            return NotFound($"Match with {id} does not exist");
        }

        return NoContent();
    }
}
