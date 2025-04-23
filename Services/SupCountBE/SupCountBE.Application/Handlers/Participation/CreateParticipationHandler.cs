using FluentValidation;
using SupCountBE.Application.Commands.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Application.Validations.Participation;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Participation;

public class CreateParticipationHandler : IRequestHandler<CreateParticipationCommand, ParticipationResponse>
{
    private readonly IParticipationRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IExpenseRepository _expenseRepository;


    public CreateParticipationHandler(IParticipationRepository repository, IMapper mapper, IUserRepository userRepository, IExpenseRepository expenseRepository)
    {
        _repository = repository;
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

        int userId = int.Parse(request.UserId);
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found.");

        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);
        if (expense == null)
            throw new Exception("Expense not found.");



        var participation = new Core.Entities.Participation
        {
            Weight = request.Weight,
            UserId = request.UserId,
            ExpenseId = request.ExpenseId
        };

        await _repository.AddAsync(participation);

        var createParticipation = await _repository.GetByIdsIncludingAsync(request.UserId, request.ExpenseId);
        ;

        return _mapper.Map<ParticipationResponse>(createParticipation);
    }
}
