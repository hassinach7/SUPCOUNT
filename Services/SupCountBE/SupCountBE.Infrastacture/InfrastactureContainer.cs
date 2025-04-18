using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupCountBE.Core.Repositories;
using SupCountBE.Infrastacture.Data.Context;
using SupCountBE.Infrastacture.Repositories;

namespace SupCountBE.Infrastacture;

public static class InfrastactureContainer
{
    public static IServiceCollection InfpRegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SupCountDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            options.UseSqlServer(connectionString);
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


        return services;
    }
}
