using AutoMapper;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.Dtos.GetDtos;

namespace MovieApiMvc.Services.Mappers;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<MovieDto, MovieEntity>();
    }
}