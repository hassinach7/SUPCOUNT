using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupCountBE.Application.Common.Services;
using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.AuthSettings;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;
using SupCountBE.Infrastacture.Services;
using SupCountBE.Infrastructure.Repositories;

namespace SupCountBE.Infrastacture;

public static class InfrastactureContainer
{
    public static IServiceCollection InfpRegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SupCountDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,             // Optional: number of retry attempts (default: 6)
                    maxRetryDelay: TimeSpan.FromSeconds(10),  // Optional: max delay between retries
                    errorNumbersToAdd: null       // Optional: specific SQL error numbers to consider transient
                );
            });
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IJustificationRepository, JustificationRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IParticipationRepository, ParticipationRepository>();
        services.AddScoped<IReimbursementRepository, ReimbursementRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserGroupRepository, UserGroupRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.Configure<JwtSettings>(options =>
        {
            var jwtSection = configuration.GetSection("JWT");
            if(jwtSection is null)
            {
                throw new ArgumentNullException(nameof(jwtSection), "JWT section not found in configuration");
            }
            options.Key = jwtSection["Key"];
            options.Issuer = jwtSection["Issuer"];
            options.Audience = jwtSection["Audience"];
            if (!int.TryParse(jwtSection["ExpirationInMinutes"], out var expiration))
            {
                throw new ArgumentException("Invalid ExpirationInMinutes value in JWT section");
            }
            options.ExpirationInMinutes = expiration;
        });
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        });

        return services;
    }
}
