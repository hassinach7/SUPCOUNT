using FluentValidation;
using SupCountBE.Application.Commands.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Application.Validations.Participation;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Participation;

public class CreateParticipationHandler : IRequestHandler<CreateParticipationCommand, ParticipationResponse>
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

    public async Task<ParticipationResponse> Handle(CreateParticipationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateParticipationValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

       

        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);
        if (expense == null)
            throw new Exception("Expense not found.");



        var participation = new Core.Entities.Participation
        {
            Weight = request.Weight,
            UserId = _participationrepository.GetCurrentUser(),
            ExpenseId = request.ExpenseId
        };

        await _participationrepository.AddAsync(participation);

        var createParticipation = await _participationrepository.GetByIdsIncludingAsync(request.ExpenseId);
        ;

        return _mapper.Map<ParticipationResponse>(createParticipation);
    }
}
