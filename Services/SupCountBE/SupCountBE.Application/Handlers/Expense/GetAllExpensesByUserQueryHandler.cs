using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense
{
    public class GetAllExpensesByUserQueryHandler : IRequestHandler<GetAllExpensesByUserQuery, List<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetAllExpensesByUserQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<List<ExpenseResponse>> Handle(GetAllExpensesByUserQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.GetAllExpensesByUserIdAsync(request.UserId);
            return _mapper.Map<List<ExpenseResponse>>(expenses);
        }
    }
   
}
