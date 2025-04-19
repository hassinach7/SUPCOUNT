using FluentValidation;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Application.Validations.Expense;
using SupCountBE.Core.Repositories;

public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, ExpenseResponse>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public CreateExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ExpenseResponse> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateExpenseValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var expense = new Expense
        {
            Title = request.Title,
            Amount = request.Amount,
            Date = request.Date,
            PayerId = request.PayerId,
            CategoryId = request.CategoryId,
            GroupId = request.GroupId
        };

        var created = await _expenseRepository.AddAsync(expense);
        var fullExpense = await _expenseRepository.GetByIdIncludingAsync(created.Id, includeGroup: true, includeCategory: true, includeParticipations: true, includeJustifications: true);
        return _mapper.Map<ExpenseResponse>(fullExpense);
    }
}
