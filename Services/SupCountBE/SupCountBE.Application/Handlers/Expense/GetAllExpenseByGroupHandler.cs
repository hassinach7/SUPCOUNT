using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense;

public class GetAllExpenseByGroupHandler : IRequestHandler<GetAllExpenseByGroupQuery, List<ExpenseResponse>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper _mapper;

    public GetAllExpenseByGroupHandler(IExpenseRepository expenseRepository, IUserRepository userRepository , IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        this.userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ExpenseResponse>> Handle(GetAllExpenseByGroupQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userRepository.GetCurrentUser();
        var expenses = await _expenseRepository.GetAllExpenseByGroupAsync(request.GroupId, currentUser);

        return _mapper.Map<List<ExpenseResponse>>(expenses);
    }
}
