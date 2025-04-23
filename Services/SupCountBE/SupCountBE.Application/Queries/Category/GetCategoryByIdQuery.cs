using SupCountBE.Application.Responses.Category;

namespace SupCountBE.Application.Queries.Category;

public class GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public int Id { get; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
