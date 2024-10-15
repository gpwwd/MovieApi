using Application.IServices;
using Domain.Entities.MovieEntities;
using Domain.Entities.UsersEntities;
using Infrastructure.Database.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;
public sealed class MovieDataBaseContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public MovieDataBaseContext(DbContextOptions<MovieDataBaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public  DbSet<RatingEntity> Ratings { get; set; }
    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<ImageInfoEntity> Images { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
        string? connectionString = config.GetConnectionString("Data Source");
        optionsBuilder.UseSqlite(connectionString);
    }
}