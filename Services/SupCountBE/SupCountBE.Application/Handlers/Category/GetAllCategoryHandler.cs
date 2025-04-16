using SupCountBE.Application.Queries.Category;
using SupCountBE.Application.Responses;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Category;

public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, IList<CategoryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IList<CategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.ListAllAsync();
        return _mapper.Map<IList<CategoryResponse>>(categories);
    }
}
