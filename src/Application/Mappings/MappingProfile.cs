using Application.Messages.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entites;

namespace Application.Mappings;

internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>();

        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
            .ForMember(dest => dest.SenderName,
                opt => opt.MapFrom(src => src.Sender.IsActive ? src.Sender.Name : "Deactivated User"))
            .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientId))
            .ForMember(dest => dest.RecipientName,
                opt => opt.MapFrom(src => src.Recipient.IsActive ? src.Recipient.Name : "Deactivated User"));
    }
}
