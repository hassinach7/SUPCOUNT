using SupCountBE.Infrastacture.Data.Context;
using System.IdentityModel.Tokens.Jwt;

namespace SupCountBE.API.Middleware;

public class ConnectedUserMiddleware
{
    private readonly RequestDelegate _next;

    public ConnectedUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, SupCountDbContext supCountDbContext)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var headerValues))
        {
            var token = headerValues.FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid");
                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;
                    supCountDbContext.UserId = userId;
                    Console.WriteLine($"The User Id Connected is : {userId}");
                }
            }

        }


        await _next(context);
    }
}
