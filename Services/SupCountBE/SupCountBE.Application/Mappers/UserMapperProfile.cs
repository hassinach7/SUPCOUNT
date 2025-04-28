
using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Responses.User;

namespace SupCountBE.Application.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
           CreateMap<User, UserResponse>();
            // Mapping User with RegisterUserCommand
            CreateMap<RegisterUserCommand, User>();
            // Mapping User with UpdateUserCommand
            CreateMap<UpdateUserCommand, User>();
               

        }
    }
}
