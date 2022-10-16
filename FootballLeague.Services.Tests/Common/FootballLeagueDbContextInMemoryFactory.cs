using FootballLeague.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Services.Tests.Common;

public static class FootballLeagueDbContextInMemoryFactory
{
    public static FootballLeagueDbContext InitializeContext()
    {
        var options = new DbContextOptionsBuilder<FootballLeagueDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        return new FootballLeagueDbContext(options);
    }
}
