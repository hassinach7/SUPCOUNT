using SupCountBE.Application.Responses.Category;

namespace SupCountBE.Application.Queries.Category;

public class GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }

   
}
