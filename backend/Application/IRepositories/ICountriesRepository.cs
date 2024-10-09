using Domain.Entities.MovieEntities;

namespace Application.IRepositories;

public interface ICountriesRepository
{
    public Task<List<CountryEntity>>? GetByName(IEnumerable<string> name, bool trackChanges);   
}