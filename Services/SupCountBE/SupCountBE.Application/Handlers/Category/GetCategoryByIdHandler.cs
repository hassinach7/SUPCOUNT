using SupCountBE.Application.Queries.Category;
using SupCountBE.Application.Responses.Category;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Category;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            throw new Exception("Category not found.");

        return _mapper.Map<CategoryResponse>(category);
    }

}
