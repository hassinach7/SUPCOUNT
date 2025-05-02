using FluentValidation;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Validations.Expense;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, int>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper, IGroupRepository groupRepository, ICategoryRepository categoryRepository )
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _groupRepository = groupRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateExpenseValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);
        var group = await _groupRepository.GetByIdAsync(request.GroupId!.Value);
        if (group == null)
            throw new ExpenseException($"The Groupe is not found ");
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId!.Value);
        if (category == null)
            throw new ExpenseException($"The category not found");

        var expense = new Expense
        {
            Title = request.Title!,
            Amount = request.Amount,
            Date = request.Date,
            PayerId = _expenseRepository.GetCurrentUser(),
            CategoryId = request.CategoryId,
            GroupId = request.GroupId
        };

        var created = await _expenseRepository.AddAsync(expense);
    
        return created.Id;
    }
}
