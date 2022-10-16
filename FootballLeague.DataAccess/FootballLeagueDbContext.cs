using FootballLeague.Data;
using FootballLeague.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FootballLeague.DataAccess;

public class FootballLeagueDbContext : DbContext
{
    public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; } = default!;

    public DbSet<Match> Matches { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}
