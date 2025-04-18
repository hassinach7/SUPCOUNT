using SupCountBE.Application.Responses.Category;

namespace SupCountBE.Application.Queries.Category;

public class GetAllCategoryQuery :IRequest<IList<CategoryResponse>>;