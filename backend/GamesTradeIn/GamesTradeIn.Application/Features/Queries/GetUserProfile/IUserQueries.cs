namespace GamesTradeIn.Application.Features.Queries.GetUserProfile;

public interface IUserQueries
{
    Task<UserProfileDto?> GetUserProfile(Guid id);
}