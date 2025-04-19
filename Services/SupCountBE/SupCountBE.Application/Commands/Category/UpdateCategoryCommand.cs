

using SupCountBE.Application.Responses.Category;

namespace SupCountBE.Application.Commands.Category
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    
    
}
