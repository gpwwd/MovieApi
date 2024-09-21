using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(b => b.Id);

        entityTypeBuilder.ToTable("Budgets");
    }
}