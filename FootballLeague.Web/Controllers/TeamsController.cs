﻿using FootballLeague.Services.Contracts;
using FootballLeague.Services.Models;
using FootballLeague.Web.Models.RequestModels;
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
    public async Task<IActionResult> GetTeam(Guid teamId, CancellationToken cancellationToken)
    {
        try
        {
            var team = await _teamsService.GetTeam(teamId, cancellationToken);

            if (team is null)
            {
                return NotFound($"Team with {teamId} does not exist");
            }

            return Ok(team);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(CreateTeamRequest), 201)]
    [HttpPost]
    public async Task<IActionResult> AddTeam(CreateTeamRequest createTeamRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var isCreated = await _teamsService.AddTeam(
                new TeamDto() { Name = createTeamRequest.Name },
                cancellationToken);

            if (!isCreated)
            {
                return BadRequest("Team was not created!");
            }

            return CreatedAtAction(nameof(AddTeam), createTeamRequest);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam([FromRoute] Guid id,
        [FromBody] UpdateTeamRequest updateTeamRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var teamDto = new TeamDto()
            {
                Id = id,
                Name = updateTeamRequest.Name,
                MatchPlayed = updateTeamRequest.MatchPlayed,
                Wins = updateTeamRequest.Wins,
                Draws = updateTeamRequest.Draws,
                Losses = updateTeamRequest.Losses
            };

            var isSuccessfullyUpdated = await _teamsService
                .UpdateTeam(teamDto,
                    cancellationToken);

            if (!isSuccessfullyUpdated)
            {
                return NotFound($"Team with {id} does not exist");
            }

            return Ok(updateTeamRequest);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var isSuccessfullyDeleted = await _teamsService
                .DeleteTeam(id, cancellationToken);

            if (!isSuccessfullyDeleted)
            {
                return NotFound($"Team with {id} does not exist");
            }

            return NoContent();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    [HttpGet]
    public async Task<IActionResult> GetRanking(CancellationToken cancellationToken)
    {
        try
        {
            var ranking = await _teamsService.GetRanking(cancellationToken);

            return Ok(ranking);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}