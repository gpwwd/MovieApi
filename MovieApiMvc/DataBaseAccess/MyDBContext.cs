using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Configurations;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;

namespace MovieApiMvc.DataBaseAccess;
public class MovieDataBaseContext : DbContext
{
    public MovieDataBaseContext(DbContextOptions<MovieDataBaseContext> options) : base(options)
    {}

    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public  DbSet<RatingEntity> Ratings { get; set; }
    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ImageInfoEntity> Images { get; set; }
    public DbSet<RoleEntitiy> Roles { get; set; }
    /// <summary>
    ///we call the OnModelCreating method from the base class.
    ///This is required for migration to work properly.
    /// </summary> 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetConfiguration());
    }
}