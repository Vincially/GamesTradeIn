using MediatR;

namespace GamesTradeIn.Application.Features.Queries.GetUserProfile;

public record GetUserProfileQuery(Guid Id) : IRequest<UserProfileDto?>;
