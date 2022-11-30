using CQRS_with_event_Sourcing_pattern.Repositories;
using CQRS_with_event_Sourcing_pattern.Repositories.Implementations;
using CQRS_with_Redis.Validation;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost"));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
