using FluentValidation;
using SupCountBE.Application.Commands.Transaction;
using SupCountBE.Application.Responses.Transaction;
using SupCountBE.Application.Validations.Transaction;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Transaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, TransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly IReimbursementRepository _reimbursementRepository;

    public CreateTransactionHandler(ITransactionRepository transactionRepository, IMapper mapper, IReimbursementRepository reimbursementRepository)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _reimbursementRepository = reimbursementRepository;
    }

    public async Task<TransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTransactionValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var reimbursement = await _reimbursementRepository.GetByIdAsync(request.ReimbursementId!.Value);
        if (reimbursement == null)
            throw new Exception("Reimbursement not found.");


        var transaction = new Core.Entities.Transaction
        {
            ReimbursementId = request.ReimbursementId.Value,
            PaymentMethod = request.PaymentMethod,
            Amount = request.Amount
        };

        await _transactionRepository.AddAsync(transaction);

        var full = await _transactionRepository.GetByIdIncludingAsync(transaction.Id, includeReimbursement: true);
        return _mapper.Map<TransactionResponse>(full);
    }
}
