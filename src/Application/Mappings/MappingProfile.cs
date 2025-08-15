using Application.Users.DTOs;
using AutoMapper;
using Domain.Entites;

namespace Application.Mappings;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>();
    }
}
