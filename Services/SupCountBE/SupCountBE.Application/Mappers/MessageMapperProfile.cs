

using SupCountBE.Application.Responses.Message;

namespace SupCountBE.Application.Mappers
{
    public  class MessageMapperProfile : Profile
    {
        public MessageMapperProfile()
        {
            this.CreateMap<Message, MessageResponse>();
        }
    }
}
