
using GamesTradeIn.API.Endpoints;
using GamesTradeIn.API.MIddlewares;
using GamesTradeIn.Application.Features.Commands.Users.CreateUser;
using GamesTradeIn.Application.Features.Queries.GetUserProfile;
using GamesTradeIn.Domain.Repositories;
using GamesTradeIn.Infrastructure;
using GamesTradeIn.Infrastructure.Data.Context;
using GamesTradeIn.Infrastructure.Queries;
using GamesTradeIn.Infrastructure.Repositories;
using JasperFx.CodeGeneration.Model;
using Microsoft.EntityFrameworkCore;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(CreateUserCommand).Assembly);
    opts.ServiceLocationPolicy = ServiceLocationPolicy.AlwaysAllowed;
    opts.Durability.Mode = DurabilityMode.MediatorOnly;
});

// Database Connection
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserQueries, UserQueries>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapUserEndpoints();
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<GamesTradeInDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();
