using SupCountBE.Application.Responses;

namespace SupCountBE.Application.Queries.Category;

public class GetAllCategoryQuery :IRequest<IList<CategoryResponse>>;