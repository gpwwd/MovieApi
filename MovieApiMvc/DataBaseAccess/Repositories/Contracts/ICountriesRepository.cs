using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.RequestFeatures;

namespace MovieApiMvc.DataBaseAccess.Repositories.Contracts;

public interface ICountriesRepository
{
    public Task<List<CountryEntity>>? GetByName(IEnumerable<string> name, bool trackChanges);   
}