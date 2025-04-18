using Microsoft.Extensions.DependencyInjection;
using SupCountBE.Application.Handlers.Category;

namespace SupCountBE.Application;

public static class ApplicationContainer
{
    public static IServiceCollection AppRegisterServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllCategoryHandler>());
        return services;
    }
}
