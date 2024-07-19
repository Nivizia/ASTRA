using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using DiamondAPI.Repositories;
using DiamondAPI.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load JWT settings from configuration
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", policy =>
    {
        policy.WithOrigins("http://astradiamonds.com:5173") // Change to match frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allow credentials if needed
    });
});

// Configure Entity Framework and SQL Server
builder.Services.AddDbContext<DiamondprojectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBDefault"));
});

// Register repositories for dependency injection
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDiamondRepository, DiamondRepository>();
builder.Services.AddScoped<IEarringRepository, EarringRepository>();
builder.Services.AddScoped<IEarringPairingRepository, EarringPairingRepository>();
builder.Services.AddScoped<IPendantRepository, PendantRepository>();
builder.Services.AddScoped<IPendantPairingRepository, PendantPairingRepository>();
builder.Services.AddScoped<IRingRepository, RingRepository>();
builder.Services.AddScoped<IRingPairingRepository, RingPairingRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderitemRepository, OrderitemRepository>();
builder.Services.AddScoped<IShapeRepository, ShapeRepository>();
builder.Services.AddScoped<IRingTypeRepository, RingTypeRepository>();
builder.Services.AddScoped<IRingSubtypeRepository, RingSubtypeRepository>();
builder.Services.AddScoped<IFrameTypeRepository, FrameTypeRepository>();
builder.Services.AddScoped<IMetalTypeRepository, MetalTypeRepository>();

// Register EmailService
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();

// Register OrderService
builder.Services.AddScoped<OrderService>();

// Configure Hangfire to use SQL Server and add Hangfire server
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBDefault"));
});
builder.Services.AddHangfireServer();

// Register TokenService for Token-Based Authentication (JWT)
builder.Services.AddScoped<TokenService>();

// Register DiamondCalculatorService for calculating diamond prices
builder.Services.AddScoped<DiamondCalculatorService>();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? string.Empty))
    };
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowFrontendOrigin");

// Use Authentication Middleware
app.UseAuthentication();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiamondAPI V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

// Add Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule a recurring job with explicit recurringJobId
RecurringJob.AddOrUpdate<OrderService>("SendOrderConfirmRequest", service => service.SendOrderConfirmRequest(), Cron.Hourly);
RecurringJob.AddOrUpdate<OrderService>("ChangeOrderPostponed", service => service.ChangeOrderPostponed(), Cron.Hourly);

// Use Authorization Middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
