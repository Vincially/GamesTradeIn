namespace GamesTradeIn.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users");

        group.MapGet("/", async () =>
        {
            return Results.Ok("This api is working!");
        })
        .WithName("GetTest");
    }
}