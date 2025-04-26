using FluentValidation;
using SupCountBE.Application.Commands.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Application.Validations.Participation;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Participation;

public class CreateParticipationHandler : IRequestHandler<CreateParticipationCommand, Unit>
{
    private readonly IParticipationRepository _participationrepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;


    public CreateParticipationHandler(IParticipationRepository participationrepository, IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository)
    {
        _participationrepository = participationrepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _expenseRepository = expenseRepository;
    }

    public async Task<Unit> Handle(CreateParticipationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateParticipationValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        // Check if the User Exist or not
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        if (user is null)
            throw new UserException($"The User with {request.UserId} is not found");

        // check if the Expense Exist or not
        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId!.Value);
        if(expense is null)
            throw new ExpenseException($"The Expense with {request.ExpenseId} is not found");

        if(user.Balance<request.Weight)
            throw new UserException($"The User with {request.UserId} has not enough balance to participate in this expense.");


        var participation = new Core.Entities.Participation
        {
            Weight = request.Weight,
            UserId =  user.Id,
            ExpenseId = expense.Id
        };

        await _participationrepository.AddAsync(participation);

        // Update User Balance
        user.Balance -= request.Weight;
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
