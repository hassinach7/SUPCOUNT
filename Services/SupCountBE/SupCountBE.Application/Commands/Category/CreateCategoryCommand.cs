using SupCountBE.Application.Responses.Category;

namespace SupCountBE.Application.Commands.Category;

public class CreateCategoryCommand :IRequest<CategoryResponse>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
