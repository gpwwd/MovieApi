using Application.Dtos.GetDtos;
using AutoMapper;
using Domain.Entities.MovieEntities;

namespace Application.Mappers;

public class GenreMapperProfile : Profile
{
    public GenreMapperProfile()
    {
        CreateMap<GenreEntity, GenreDto>();
    }
} 