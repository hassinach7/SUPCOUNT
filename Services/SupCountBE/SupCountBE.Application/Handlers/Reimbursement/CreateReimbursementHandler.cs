using FluentValidation;
using SupCountBE.Application.Commands.Reimbursement;
using SupCountBE.Application.Responses.Reimbursement;
using SupCountBE.Application.Validations.Reimbursement;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Reimbursement
{
    public class CreateReimbursementHandler : IRequestHandler<CreateReimbursementCommand, ReimbursementResponse>
    {
        private readonly IReimbursementRepository _reimbursementRepository;
        private readonly IMapper _mapper;

        public CreateReimbursementHandler(IReimbursementRepository reimbursementRepository, IMapper mapper)
        {
            _reimbursementRepository = reimbursementRepository;
            _mapper = mapper;
        }

        public async Task<ReimbursementResponse> Handle(CreateReimbursementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateReimbursementValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var reimbursement = new Core.Entities.Reimbursement
            {
                Name = request.Name,
                SenderId = _reimbursementRepository.GetCurrentUser(),
                BeneficiaryId = request.BeneficiaryId,
                Amount = request.Amount,
                GroupId = request.GroupId
            };

            await _reimbursementRepository.AddAsync(reimbursement);

            var full = await _reimbursementRepository.GetByIdIncludingAsync(
                reimbursement.Id,
                new ReimbursementIncludingProperties
                {
                    IncludeSenders = true,
                    IncludeBeneficiaries = true,
                    IncludeGroups = true,
                    IncludeTransactions = true
                });

            return _mapper.Map<ReimbursementResponse>(full);
        }
    }
}
