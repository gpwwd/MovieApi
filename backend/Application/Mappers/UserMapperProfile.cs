using Application.Dtos.GetDtos;
using Application.Dtos.PostDtos;
using Application.Dtos.UpdateDtos;
using AutoMapper;
using Domain.Entities.UsersEntities;

namespace Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDto, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt
                => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt
                => opt.Ignore());
        
        CreateMap<UserForRegistrationDto, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt
                => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt
                => opt.Ignore());
        
        CreateMap<UserUpdateDto, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt
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