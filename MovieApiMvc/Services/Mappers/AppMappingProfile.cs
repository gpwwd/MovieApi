using AutoMapper;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;

namespace MovieApiMvc.Services.Mappers;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<MovieDto, MovieEntity>()
            .ForMember(dest => dest.Genres, opt
                => opt.MapFrom(src => src.Genres!.Select(name => new GenreEntity { Name = name })))
            .ForMember(dest => dest.Countries, opt
                => opt.MapFrom(src => src.Countries!.Select(name => new CountryEntity { Name = name })))
            .ForMember(dest => dest.ImageInfoEntity, opt
                => opt.MapFrom(src => src.ImageInfo!))
            .ReverseMap()
            .ForMember(dest => dest.Genres, opt
                => opt.MapFrom(src => src.Genres!.Select(g => g.Name)))
            .ForMember(dest => dest.Countries, opt
                => opt.MapFrom(src => src.Countries!.Select(c => c.Name)))
            .ForMember(dest => dest.ImageInfo, opt
                => opt.MapFrom(src => src.ImageInfoEntity));

        CreateMap<RatingDto, RatingEntity>()
            .ReverseMap();
        CreateMap<BudgetDto, BudgetEntity>()
            .ReverseMap();
        CreateMap<ImageInfoDto, ImageInfoEntity>()
            .ReverseMap();


        CreateMap<PostMovieDto, MovieEntity>()
            .ForMember(dest => dest.Countries, opt
                => opt.Ignore())
            .ForMember(dest => dest.Genres, opt
                => opt.Ignore())
            .ForMember(dest => dest.Rating, opt
                => opt.Ignore());
    }
}