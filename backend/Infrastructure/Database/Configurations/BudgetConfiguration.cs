using Domain.Entities.MovieEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(b => b.Id);

        entityTypeBuilder.ToTable("Budgets");
    }
}