using FootballLeague.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballLeague.DataAccess.Configurations;

internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasQueryFilter(t => !t.IsDeleted);

        builder
            .HasKey(t => t.Id);

        builder
            .HasIndex(t => t.Name)
            .IsUnique();

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
