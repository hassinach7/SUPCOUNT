using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupCountBE.Infrastacture.Data.Context;

namespace SupCountBE.Infrastacture;

public static class InfrastactureContainer
{
    public static IServiceCollection AddInfrastacture(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SupCountDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
