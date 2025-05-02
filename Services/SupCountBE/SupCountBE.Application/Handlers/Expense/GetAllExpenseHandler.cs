using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Responses.Expense;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Expense;

public class GetAllExpenseHandler : IRequestHandler<GetAllExpenseQuery, IList<ExpenseResponse>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;

    public GetAllExpenseHandler(IExpenseRepository expenseRepository, IMapper mapper, IUserGroupRepository userGroupRepository
        , IUserRepository userRepository)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
    }

    public async Task<IList<ExpenseResponse>> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
    {
        var expenses = await _expenseRepository.GetAllListIncludingAsync(new IncludingProperties
        {
            IncludePayer = true,
            IncludeCategory = true,
            IncludeGroup = true,
            IncludeParticipations = true,
            IncludeJustifications = true
        });
        var mappingExpenses =  _mapper.Map<IList<ExpenseResponse>>(expenses);
        foreach (var item in mappingExpenses)
        {
            var userGroup = await _userGroupRepository.GetListByGroupIdAsync(item.Group.Id, true);
            if(userGroup != null)
            {
                item.Members = userGroup.Select(x => x.User!.FullName).ToList();
            }
        }

        return mappingExpenses;
    }
}
