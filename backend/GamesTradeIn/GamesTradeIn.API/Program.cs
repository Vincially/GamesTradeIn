
using GamesTradeIn.API.Endpoints;
using GamesTradeIn.API.MIddlewares;
using GamesTradeIn.Application.Features.Commands.Users.CreateUser;
using GamesTradeIn.Application.Features.Queries.GetUserProfile;
using GamesTradeIn.Domain.Repositories;
using GamesTradeIn.Infrastructure;
using GamesTradeIn.Infrastructure.Queries;
using GamesTradeIn.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetUserProfileQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
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

app.Run();
