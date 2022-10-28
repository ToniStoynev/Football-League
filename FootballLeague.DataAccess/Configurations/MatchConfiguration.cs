using FootballLeague.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballLeague.DataAccess.Configurations;

internal class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasQueryFilter(m => !m.IsDeleted);

        builder.HasOne(m => m.HomeTeam)
            .WithMany(t => t.Matches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasKey(m => m.Id);

        builder
            .Property(m => m.HomeTeamId)
            .IsRequired();

        builder
            .Property(m => m.AwayTeamId)
            .IsRequired();

    }
}
