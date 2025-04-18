using FluentValidation;
using SupCountBE.Application.Commands.Category;
using SupCountBE.Application.Responses.Category;
using SupCountBE.Application.Validations.Category;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Category;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validations = new CreateCategoryValidator();
        var validationResult = await validations.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var createdCatgeory = await _categoryRepository.AddAsync(new Core.Entities.Category { Name = request.Name });

        return _mapper.Map<CategoryResponse>(createdCatgeory);
    }
}
