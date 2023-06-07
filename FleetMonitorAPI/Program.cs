using FleetMonitorAPI.Entities;
using System.Reflection;
using FleetMonitorAPI.Services;
using NLog.Web;
using FleetMonitorAPI.Middleware;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using FleetMonitorAPI.Models;
using FleetMonitorAPI.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FleetMonitorAPI.Utilities;
using FleetMonitorAPI.Models.Queries;
using FleetMonitorAPI.Models.Login;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//NLog config
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//Authentication settings
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option => 
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg => 
{
    cfg.RequireHttpsMetadata= false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddDbContext<FleetMonitorDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FleetMonitorDbConnection")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IVehcileService, VehicleService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<PaginationQuery>, DeviceQueryValidator>();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => 
{
    options.AddPolicy("FrontendClient", corsBuilder =>
        corsBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    ); 
});

var app = builder.Build();
//Configure
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
app.UseCors("FrontendClient");

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FleetMonitor API");
});

app.UseAuthorization();

app.MapControllers();
seeder.Seed();
app.Run();
