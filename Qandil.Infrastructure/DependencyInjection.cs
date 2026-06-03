using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;
using Qandil.Infrastructure.Repositories;

namespace Qandil.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        // Uow
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(config.GetConnectionString("DefaultConnetion")));

        return services;
    }

}