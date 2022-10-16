using FootballLeague.DataAccess;
using FootballLeague.Services.Implementations;
using FootballLeague.Services.Models;
using FootballLeague.Services.Tests.Common;
using Xunit;

namespace FootballLeague.Services.Tests;

public class TeamsServiceTests
{
    private readonly FootballLeagueDbContext _dbContext;

    public TeamsServiceTests()
    {
        _dbContext = FootballLeagueDbContextInMemoryFactory.InitializeContext();
    }

    [Fact]
    public async Task AddTeam_WithNullParamater_ShouldReturnFalse()
    {
        //Arrage
        var teamsService = new TeamsService(_dbContext);

        //Act
        var result = await teamsService.AddTeam(null, new CancellationToken());

        //Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddTeam_WithValidParamater_ShouldReturnTrue()
    {
        //Arrage
        var teamsService = new TeamsService(_dbContext);
        var teamDto = new TeamDto { Name = "Test team name" };

        //Act
        var result = await teamsService.AddTeam(teamDto, new CancellationToken());

        //Assert
        Assert.True(result);
    }
}
