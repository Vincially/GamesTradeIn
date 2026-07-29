namespace GamesTradeIn.Application.Features.Queries.GetUserProfile;

public class GetUserProfileQueryHandler(IUserQueries userQueries)
{
    public async Task<UserProfileDto?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        return await userQueries.GetUserProfile(request.Id);
    }
}