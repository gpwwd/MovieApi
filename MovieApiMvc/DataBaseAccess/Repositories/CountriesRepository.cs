using Microsoft.EntityFrameworkCore;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

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