using SupCountBE.Application.Commands.Security;
using SupCountBE.Application.Common.Services;
using SupCountBE.Application.Dtos;

namespace SupCountBE.Application.Handlers.Security
{
    public class GoogleAuthCommandHandler : IRequestHandler<GoogleAuthCommand, LoginAuthResponseDto>
    {
      
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        public GoogleAuthCommandHandler(ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<LoginAuthResponseDto> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
        {
            var authModel = await _tokenGenerator.GetExternalTokenAsync(request.Email, "Google", request.FullName);

            return _mapper.Map<LoginAuthResponseDto>(authModel);
        }
    }


    
}
