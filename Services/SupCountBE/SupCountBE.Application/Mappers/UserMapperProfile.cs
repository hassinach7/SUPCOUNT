
using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Responses.User;

namespace SupCountBE.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
           this.CreateMap<User, UserResponse>();
            // Mapping User with RegisterUserCommand
            this.CreateMap<RegisterUserCommand, User>();

        }
    }
}
