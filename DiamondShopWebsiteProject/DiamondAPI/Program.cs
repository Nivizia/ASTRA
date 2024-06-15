using DiamondAPI.Data;
using DiamondAPI.Interfaces;
using DiamondAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Allow requests from this frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<DiamondprojectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IDiamondRepository, DiamondRepository>();
builder.Services.AddScoped<IEarringRepository, EarringRepository>();
builder.Services.AddScoped<IPendantRepository, PendantRepository>();
builder.Services.AddScoped<IRingRepository, RingRepository>();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowFrontendOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
