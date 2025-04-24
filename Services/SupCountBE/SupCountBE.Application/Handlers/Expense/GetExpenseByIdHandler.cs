using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense
{
    public class GetExpenseByIdHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetExpenseByIdHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseResponse> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetByIdIncludingAsync(
                request.Id,
                includePayer: true,
                includeGroup: true,
                includeCategory: true,
                includeParticipations: true,
                includeJustifications: true
            );

            if (expense == null)
                throw new Exception("Expense not found.");

            return _mapper.Map<ExpenseResponse>(expense);
        }
    }
}
