using Qandil.API.Extensions;

namespace Qandil.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration config)
        {
           // services.AddCustomSwagger();
            services.AddCustomCors(config);
            return services;
        }
    }
}
