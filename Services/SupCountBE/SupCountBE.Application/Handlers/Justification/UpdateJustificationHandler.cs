using FluentValidation;
using SupCountBE.Application.Commands.Justification;
using SupCountBE.Application.Responses.Justification;
using SupCountBE.Application.Validations.Justification;
using SupCountBE.Core.Repositories;


namespace SupCountBE.Application.Handlers.Justification
{
    public class UpdateJustificationHandler : IRequest<JustificationResponse>
    {
        private readonly IJustificationRepository _justificationRepository;
        private readonly IMapper _mapper;
        public UpdateJustificationHandler(IJustificationRepository justificationRepository, IMapper mapper)
        {
            _justificationRepository = justificationRepository;
            _mapper = mapper;
        }
        public async Task<JustificationResponse> Handle(UpdateJustificationCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateJustificationValidator();
            var validationResult = await validation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
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

            


           
        
