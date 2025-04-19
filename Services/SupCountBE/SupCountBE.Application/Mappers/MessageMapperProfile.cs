using AutoMapper;
using SupCountBE.Application.Responses.Message;
using SupCountBE.Core.Entities;

namespace SupCountBE.Application.Mappers
{
    public class MessageMapperProfile : Profile
    {
        public MessageMapperProfile()
        {
            CreateMap<Message, MessageResponse>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.FullName : ""))
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Recipient != null ? src.Recipient.FullName : null))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group != null ? src.Group.Name : null));
        }
    }
}
 