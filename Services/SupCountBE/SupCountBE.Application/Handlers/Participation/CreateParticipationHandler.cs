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

    public CreateParticipationHandler(IParticipationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ParticipationResponse> Handle(CreateParticipationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateParticipationValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var participation = new Core.Entities.Participation
        {
            Weight = request.Weight,
            UserId = request.UserId,
            ExpenseId = request.ExpenseId
        };

        await _repository.AddAsync(participation);

        var createParticipation = await _repository.GetByIdsAsync(request.UserId, request.ExpenseId);

        return _mapper.Map<ParticipationResponse>(createParticipation);
    }
}
