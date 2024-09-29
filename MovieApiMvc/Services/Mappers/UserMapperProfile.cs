using AutoMapper;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;
using MovieApiMvc.Models.Dtos;
using MovieApiMvc.Models.Dtos.GetDtos;
using MovieApiMvc.Models.Dtos.PostDtos;
using MovieApiMvc.Models.Dtos.UpdateDtos;

namespace MovieApiMvc.Services.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDto, UserEntity>()
            .ForMember(dest => dest.PasswHash, opt
                => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt
                => opt.Ignore());
        
        CreateMap<UserForRegistrationDto, UserEntity>()
            .ForMember(dest => dest.PasswHash, opt
                => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt
                => opt.Ignore());
        
        CreateMap<UserUpdateDto, UserEntity>()
            .ForMember(dest => dest.PasswHash, opt
                => opt.Ignore())
            .ForMember(dest => dest.WatchLaterMovies, opt
                => opt.Ignore())
            .ForMember(dest => dest.FavMovies, opt 
                => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt
                => opt.Ignore());
    }
}