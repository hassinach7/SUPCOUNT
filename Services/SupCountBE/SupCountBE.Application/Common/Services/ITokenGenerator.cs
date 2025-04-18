using SupCountBE.Application.Common.Models;

namespace SupCountBE.Application.Common.Services;

public interface ITokenGenerator
{
    Task<AuthModel> GetTokenAsync(TokenRequestModel model);
}
