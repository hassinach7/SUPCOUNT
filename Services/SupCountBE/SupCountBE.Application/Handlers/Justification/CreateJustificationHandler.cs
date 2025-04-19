using FluentValidation;
using SupCountBE.Application.Commands.Justification;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Application.Validations.Justification;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Justification;

public class CreateJustificationHandler : IRequestHandler<CreateJustificationCommand, JustificationResponse>
{
    private readonly IJustificationRepository _repository;
    private readonly IMapper _mapper;

    public CreateJustificationHandler(IJustificationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<JustificationResponse> Handle(CreateJustificationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateJustificationValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var justification = new Core.Entities.Justification
        {
            ExpenseId = request.ExpenseId,
            FileContent = request.FileContent,
            Type = request.Type
        };

        await _repository.AddAsync(justification);

        var full = await _repository.GetByIdIncludingAsync(justification.Id, includeExpense: true);
        return _mapper.Map<JustificationResponse>(full);
    }
}
