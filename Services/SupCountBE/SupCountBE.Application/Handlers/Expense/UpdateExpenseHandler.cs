using FluentValidation;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Validations.Expense;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense
{
    public class UpdateExpenseHandler : IRequestHandler<UpdateExpenseCommand, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper, IGroupRepository groupRepository, ICategoryRepository categoryRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _groupRepository = groupRepository;
        }

        public async Task<ExpenseResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExpenseValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var expense = await _expenseRepository.GetByIdAsync(request.Id!.Value);
            if (expense is null)
                throw new Exception("Expense not found.");
            var group = await _groupRepository.GetByIdAsync(request.GroupId!.Value);
            if (group == null)
                throw new ExpenseException($"Group not found");
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);
            if(category == null)
                throw new ExpenseException($"Category not found");

            expense.Title = request.Title;
            expense.Amount = request.Amount;
            expense.Date = request.Date;
            expense.CategoryId = request.CategoryId;
            expense.GroupId = request.GroupId;
            expense.UpdatdAt = DateTime.Now;

            await _expenseRepository.UpdateAsync(expense);

            var updated = await _expenseRepository.GetByIdIncludingAsync(
                expense.Id,
                includeGroup: true,
                includeCategory: true,
                includePayer: true,
                includeParticipations: true,
                includeJustifications: true
            );

            return _mapper.Map<ExpenseResponse>(updated!);
        }
    }
}
