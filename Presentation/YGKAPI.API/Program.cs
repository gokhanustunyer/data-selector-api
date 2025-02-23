using YGKAPI.API.Filters;
using YGKAPI.Persistence;
using YGKAPI.Infrastructure;
using YGKAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using YGKAPI.API.Middlewares;
using YGKAPI.Persistence.Seed;
using YGKAPI.API;
using Serilog;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Loggly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RolePermissionFilter>();
});
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    //policy.WithOrigins("https://localhost:4200", "http://localhost:4200", "https://localhost:4201", "http://localhost:4201").AllowAnyHeader().AllowAnyMethod()
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AppPresentationServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var logglyConfiguration = new LogglyConfiguration()
{
    ApplicationName = builder.Configuration["Logging:Loggly:applicationName"],
    CustomerToken = builder.Configuration["Logging:Loggly:customerToken"],
    EndpointHostName = builder.Configuration["Logging:Loggly:endpointHostName"],
    Tags = builder.Configuration.GetSection("Logging:Loggly:tags").Get<List<string>>(),
};

var log = new LoggerConfiguration()
                 .WriteTo.Console()
                 .WriteTo.Loggly(logglyConfig: logglyConfiguration)
                 .CreateLogger();

builder.Host.UseSerilog(log);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.SeedDatabase();

app.Run();
