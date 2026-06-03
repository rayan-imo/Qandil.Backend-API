using Qandil.API;
using Qandil.API.Filters;
using Qandil.API.Middlewares;
using Qandil.Infrastructure;
using Qandil.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddAPI(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddService(configuration);


//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new HttpResponseExceptionFilter());
//});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();

//app.UseMiddleware<RequestLoggingMiddleware>();


app.Run();
