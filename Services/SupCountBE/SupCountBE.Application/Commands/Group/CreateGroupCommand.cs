using SupCountBE.Application.Responses.Group;

namespace SupCountBE.Application.Commands.Group
{
    public class CreateGroupCommand : IRequest<GroupResponse>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    
}
