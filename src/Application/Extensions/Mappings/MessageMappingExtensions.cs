using Application.Messages.DTOs;
using Domain.Entites;

namespace Application.Extensions.Mappings;

internal static class MessageMappingExtensions
{
    public static MessageDto ToDto(this Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            SenderName = GetName(message.Sender),
            SenderId = message.SenderId,
            RecipientName = GetName(message.Recipient),
            RecipientId = message.RecipientId,
            Content = message.Content,
            DateRead = message.DateRead,
            CreatedAt = message.CreatedAt,
        };
    }

    private static string GetName(AppUser user)
    {
        return user.IsActive ? user.Name : "Deactivated User";
    }
}
