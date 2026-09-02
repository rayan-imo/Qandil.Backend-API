using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Qandil.Core.AuthServices.Hasher;
using Qandil.Core.Interfacres.EmailService;
using Qandil.Infrastructure.Service.EmailService;
using Qandil.Service.IServices;
using Qandil.Service.Services;
using Qandil.Services.AuthServices.GenerateToken;
using Qandil.Services.AuthServices.Hasher;
using Qandil.Services.AuthServices.Helper;
using Qandil.Services.AuthServices.Service;
using Qandil.Services.AuthServices.Services;
using System.Security.Claims;
using System.Text;

namespace Qandil.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JWT>(config.GetSection("JWT")); services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IChildService, ChildService>();
        services.AddScoped<ILevelService, LevelService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<ISchoolService, SchoolService>();
        services.AddScoped<IClassroomService, ClassroomService>();
        services.AddScoped<IEduProgramService, EduProgramService>();
        services.AddScoped<IDiagnosisService, DiagnosisService>();
        services.AddScoped<IDisabilityService, DisabilityService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IDiagnosisQuestionService, DiagnosisQuestionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IGenerateTokenJwt, GenerateTokenJwt>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IChildTestService, ChildTestService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChildSiteService, ChildSiteService>();

        return services;

    }
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JWT>(config.GetSection("JWT"));

        var jwt = config.GetSection("JWT").Get<JWT>();

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
                RoleClaimType = ClaimTypes.Role
            };
        });
        return services;
    }

}
