using FluentValidation;
using SupCountBE.Application.Commands.Transaction;
using SupCountBE.Application.Responses.Transaction;
using SupCountBE.Application.Validations.Transaction;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Transaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, TransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public UpdateTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateTransactionValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var transaction = await _transactionRepository.GetByIdAsync(request.Id);
        if (transaction is null)
            throw new Exception("Transaction not found.");

        transaction.PaymentMethod = request.PaymentMethod;
        transaction.Amount = request.Amount;

        await _transactionRepository.UpdateAsync(transaction);

        var full = await _transactionRepository.GetByIdIncludingAsync(transaction.Id, includeReimbursement: true);
        return _mapper.Map<TransactionResponse>(full);
    }
}
