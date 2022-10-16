using FootballLeague.DataAccess;

namespace FootballLeague.Web.Extensions;

public static class WebBuilderExtensions
{
    public static void CreateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FootballLeagueDbContext>();

        dbContext.Database.EnsureCreated();
    }
}
