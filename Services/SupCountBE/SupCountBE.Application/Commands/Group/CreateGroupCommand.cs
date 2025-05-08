namespace SupCountBE.Application.Commands.Group
{
    public class CreateGroupCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    
}
