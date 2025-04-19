using FluentValidation;
using SupCountBE.Application.Commands.Participation;
using SupCountBE.Application.Responses.Participation;
using SupCountBE.Application.Validations.Participation;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Participation;

public class UpdateParticipationHandler : IRequestHandler<UpdateParticipationCommand, ParticipationResponse>
{
    private readonly IParticipationRepository _repository;
    private readonly IMapper _mapper;

    public UpdateParticipationHandler(IParticipationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ParticipationResponse> Handle(UpdateParticipationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateParticipationValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var participation = await _repository.GetByIdsAsync(request.UserId, request.ExpenseId);
        if (participation == null)
            throw new Exception("Participation not found.");

        participation.Weight = request.Weight;

        await _repository.UpdateAsync(participation);

        return _mapper.Map<ParticipationResponse>(participation);
    }
}
