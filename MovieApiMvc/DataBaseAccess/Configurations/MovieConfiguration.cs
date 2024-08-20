using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(m => m.Id);

        entityTypeBuilder.HasOne(m => m.Budget)
            .WithOne(b => b.Movie)
            .HasForeignKey<BudgetEntity>(b => b.MovieId);

        entityTypeBuilder.ToTable("Movies");

        entityTypeBuilder.HasOne(m => m.Rating)
            .WithOne(r => r.Movie)
            .HasForeignKey<RatingEntity>(r => r.MovieId);

        entityTypeBuilder.HasMany(m => m.Countries)
            .WithMany(c => c.Movies);

        entityTypeBuilder.HasMany(m => m.Genres)
            .WithMany(g => g.Movies);
            
        entityTypeBuilder.HasOne(m => m.imageInfoEntity)
            .WithOne(i => i.Movie)
            .HasForeignKey<ImageInfoEntity>(i=>i.MovieId);
    
    }
}

