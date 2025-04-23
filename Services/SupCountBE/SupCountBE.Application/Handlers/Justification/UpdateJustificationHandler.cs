using FluentValidation;
using SupCountBE.Application.Commands.Justification;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Application.Validations.Justification;
using SupCountBE.Core.Exceptions;
using SupCountBE.Core.Repositories;


namespace SupCountBE.Application.Handlers.Justification
{
    public class UpdateJustificationHandler : IRequestHandler<UpdateJustificationCommand, JustificationResponse>
    {
        private readonly IJustificationRepository _justificationRepository;
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        public UpdateJustificationHandler(IJustificationRepository justificationRepository, IMapper mapper, IExpenseRepository expenseRepository)
        {
            _justificationRepository = justificationRepository;
            _mapper = mapper;
            _expenseRepository = expenseRepository;
        }
        public async Task<JustificationResponse> Handle(UpdateJustificationCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateJustificationValidator();
            var validationResult = await validation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId!.Value);
            if (expense == null)
                throw new JustificationException($"Expense not found.");

            var justification = await _justificationRepository.GetByIdAsync(request.Id);
            if (justification is null)

                throw new Exception("Justification not found");
            
            
            justification.FileContent = request.FileContent;
            justification.Type = request.Type;

            await _justificationRepository.UpdateAsync(justification);

            var full = await _justificationRepository.GetByIdIncludingAsync(justification.Id, includeExpense: true);
            return _mapper.Map<JustificationResponse>(full);

        }
    }
    }

            


           
        
