using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using AutoMapper;
using Domain.Entities.MovieEntities;

namespace Application.Mappers;

public class MovieMapperProfile : Profile
{
    public MovieMapperProfile()
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

        ///////////////////////////////POST_DTOS
        CreateMap<PostMovieDto, MovieEntity>()
            .ForMember(dest => dest.Countries, opt
                => opt.Ignore())
            .ForMember(dest => dest.Genres, opt
                => opt.Ignore())
            .ForMember(dest => dest.ImageInfoEntity, opt
                => opt.MapFrom(src => src.ImageInfo))
            .ForMember(dest => dest.Budget, opt 
                => opt.MapFrom(src => src.Budget));
            
        CreateMap<RatingPostDto, RatingEntity>()
            .ForMember(dest => dest.Id, opt
                => opt.Ignore())
            .ForMember(dest => dest.MovieId, opt
                => opt.Ignore())
            .ReverseMap();
        
        CreateMap<BudgetPostDto, BudgetEntity>()
            .ForMember(dest => dest.Id, opt
                => opt.Ignore())
            .ForMember(dest => dest.MovieId, opt
                => opt.Ignore())
            .ReverseMap();
        
        CreateMap<ImagePostDto, ImageInfoEntity>()
            .ForMember(dest => dest.Id, opt
                => opt.Ignore())
            .ForMember(dest => dest.MovieId, opt
                => opt.Ignore())
            .ReverseMap();
        
        
        //////////////////////////////////////UPDATE_DTOS
        CreateMap<UpdateMovieDto, MovieEntity>()
            .ForMember(dest => dest.Countries, opt
                => opt.Ignore())
            .ForMember(dest => dest.Genres, opt
                => opt.Ignore())
            .ForMember(dest => dest.Rating, opt
                => opt.Ignore())
            .ForMember(dest => dest.ImageInfoEntity, opt
                => opt.MapFrom(src => src.ImageInfo))
            .ForMember(dest => dest.Budget, opt 
                => opt.MapFrom(src => src.Budget));

    }
}