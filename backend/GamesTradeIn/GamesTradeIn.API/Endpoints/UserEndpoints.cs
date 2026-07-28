using GamesTradeIn.Application.Features.Commands.Users.CreateUser;
using GamesTradeIn.Application.Features.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamesTradeIn.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users");

        group.MapPost("/", async (
        [FromBody] CreateUserCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
            {
                var userId = await mediator.Send(command, cancellationToken);
                return Results.CreatedAtRoute("GetUserByIdAsync", new { id = userId }, new { Id = userId });
            }
        );
            
        group.MapGet("/{id:guid}", async (
                Guid id,
                IMediator mediator,
                CancellationToken cancellationToken) =>
            {
                var query = new GetUserProfileQuery(id);
                var profile = await mediator.Send(query, cancellationToken);
                
                return profile is not null
                    ? Results.Ok(profile)
                    : Results.NotFound(new { Message = "User not found" });
            })
        .WithName("GetUserProfile");
    }
}