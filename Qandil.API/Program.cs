using Qandil.API;
using Qandil.API.Extensions;
using Qandil.API.Filters;
using Qandil.Infrastructure;
using Qandil.Infrastructure.Data;
using Qandil.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCustomSwagger();
builder.Services.AddCustomCors(configuration);
builder.Services.AddAPI(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddService(configuration);
builder.Services.AddAuthentication(configuration);
builder.Services.AddAuthorization();



builder.Services.AddControllers(options =>
{
    options.Filters.Add(new HttpResponseExceptionFilter()); 
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedSuperAdminAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

//app.UseMiddleware<RequestLoggingMiddleware>();


app.Run();
