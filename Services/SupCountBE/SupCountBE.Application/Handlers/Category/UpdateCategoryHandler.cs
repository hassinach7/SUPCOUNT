using FluentValidation;
using SupCountBE.Application.Commands.Category;
using SupCountBE.Application.Responses.Category;
using SupCountBE.Application.Validations.Category;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Category
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdatecategoryValidator();
            var validationResult = await validation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);

            }
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
                throw new Exception("Category not found.");
            category.Name = request.Name;

            await _categoryRepository.UpdateAsync(category);

            return _mapper.Map<CategoryResponse>(category);
        }

    }
}
