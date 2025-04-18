using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupCountBE.Application.Common.Models;
using SupCountBE.Application.Common.Services;
using SupCountBE.Infrastacture.AuthSettings;
using SupCountBE.Infrastacture.Data.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupCountBE.Infrastacture.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly UserManager<User> _userManager;
    private readonly SupCountDbContext _dbContext;
    private readonly JwtSettings _jwtSettings;


    public TokenGenerator(UserManager<User> userManager, SupCountDbContext dbContext, IOptions<JwtSettings> options)
    {
        this._userManager = userManager;
        this._dbContext = dbContext;
        this._jwtSettings = options.Value;
    }
    public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
    {
        var authModel = new AuthModel();
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            authModel.Message = "Invalid credentials";
            return authModel;
        }
        var jwtSecurityToken = await CreateJwtToken(user);
        var rolesList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.UserName = user.UserName;
        authModel.Email = user.Email;
        authModel.UserId = user.Id;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Roles = rolesList.ToList();
        return authModel;
    }
    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var userClaimns = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaimns = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaimns.Add(new Claim(ClaimTypes.Role, role));
        }
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        }.Union(userClaimns).Union(roleClaimns);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: signingCredentials
        );
        return jwtSecurityToken;
    }

}