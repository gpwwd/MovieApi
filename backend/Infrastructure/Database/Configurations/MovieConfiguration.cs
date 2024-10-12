using Domain.Entities.MovieEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(m => m.Id);

        entityTypeBuilder.HasOne(m => m.Budget)
            .WithOne(b => b.Movie)
            .HasForeignKey<BudgetEntity>(b => b.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        entityTypeBuilder.ToTable("Movies");

        entityTypeBuilder.HasOne(m => m.Rating)
            .WithMany(r => r.Movies)
            .OnDelete(DeleteBehavior.Restrict);

        entityTypeBuilder.HasMany(m => m.Countries)
            .WithMany(c => c.Movies);

        entityTypeBuilder.HasMany(m => m.Genres)
            .WithMany(g => g.Movies);
            
        entityTypeBuilder.HasOne(m => m.ImageInfoEntity)
            .WithOne(i => i.Movie)
            .HasForeignKey<ImageInfoEntity>(i=>i.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

