using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense;

public class GetAllExpenseHandler : IRequestHandler<GetAllExpenseQuery, IList<ExpenseResponse>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetAllExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<IList<ExpenseResponse>> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
    {
        var expenses = await _expenseRepository.GetAllListIncludingAsync(includeGroup:true,includeJustifications:true,includeParticipations:true);
        return _mapper.Map<IList<ExpenseResponse>>(expenses);
    }
}
