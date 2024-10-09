using Application.IRepositories;
using Domain.Entities.MovieEntities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CountriesRepository : RepositoryBase<CountryEntity>, ICountriesRepository
{
    public CountriesRepository(MovieDataBaseContext context) : base(context)
    {
    }

    public async Task<List<CountryEntity>>? GetByName(IEnumerable<string> name, bool trackChanges)
    {
        return await FindByCondition(c => name.Contains(c.Name), trackChanges)
            .ToListAsync();
    }
}