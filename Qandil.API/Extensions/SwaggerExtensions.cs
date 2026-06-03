using Microsoft.OpenApi.Models;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;


namespace Qandil.API.Extensions
{
    public static  class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Basic Info
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Qandeel WEBSITE → API Documentation",
                    Version = "v1.0.0"
                });

               // Enhancements
               // options.EnableAnnotations();
                options.CustomSchemaIds(type => type.ToString());
               // options.AddEnumsWithValuesFixFilters();
              // options.ParameterFilter<QueryArrayParamFilter>();
               // options.IncludeXmlCommentsFromInheritDocs(true, typeof(string));

                // Security Definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Security Requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
            });

            return services;
        }
    }
}
