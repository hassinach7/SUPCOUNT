using SupCountBE.Application.Responses.Group;

namespace SupCountBE.Application.Commands.Group
{
    public class UpdateGroupCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
