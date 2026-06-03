using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qandil.Service.IServices;
using Qandil.Service.Services;

namespace Qandil.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IEmployeeService,EmployeeService>();
        services.AddScoped<IChildService,ChildService>();

        return services;
    }

}
