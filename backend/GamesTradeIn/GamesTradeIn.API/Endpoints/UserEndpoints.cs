using GamesTradeIn.Application.Features.Commands.Users.CreateUser;
using GamesTradeIn.Application.Features.Queries.GetUserProfile;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace GamesTradeIn.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users");

        group.MapPost("/", async (
        [FromBody] CreateUserCommand command,
            IMessageBus bus,
            CancellationToken cancellationToken) =>
            {
                var userId = await bus.InvokeAsync<CreateUserCommand>(command, cancellationToken);
                return Results.CreatedAtRoute("GetUserByIdAsync", new { id = userId }, new { Id = userId });
            }
        );
            
        group.MapGet("/{id:guid}", async (
                Guid id,
                IMessageBus bus,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserProfileQuery(id);
                var profile = await bus.InvokeAsync<GetUserProfileQuery>(query);
                
                return profile is not null
                    ? Results.Ok(profile)
                    : Results.NotFound(new { Message = "User not found" });
            })
        .WithName("GetUserProfile");
    }
}