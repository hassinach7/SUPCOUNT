using AutoMapper;
using FluentValidation;
using MediatR;
using SupCountBE.Application.Commands.Reimbursement;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Application.Validations.Reimbursement;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Reimbursement;

public class UpdateReimbursementHandler : IRequestHandler<UpdateReimbursementCommand, ReimbursementResponse>
{
    private readonly IReimbursementRepository _repository;
    private readonly IMapper _mapper;

    public UpdateReimbursementHandler(IReimbursementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReimbursementResponse> Handle(UpdateReimbursementCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateReimbursementValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var reimbursement = await _repository.GetByIdAsync(request.Id);
        if (reimbursement is null)
            throw new Exception("Reimbursement not found.");

        reimbursement.Name = request.Name;
        reimbursement.Amount = request.Amount;

        await _repository.UpdateAsync(reimbursement);

        var full = await _repository.GetByIdIncludingAsync(
            reimbursement.Id,
            includeSender: true,
            includeBeneficiary: true,
            includeGroup: true,
            includeTransactions: true
        );

        return _mapper.Map<ReimbursementResponse>(full);
    }
}

