namespace Qandil.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Qandeel", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
    
                });
            });

            return services;
        }
    }
}
