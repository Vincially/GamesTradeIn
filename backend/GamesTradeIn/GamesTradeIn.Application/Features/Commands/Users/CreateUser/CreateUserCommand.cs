using MediatR;

namespace GamesTradeIn.Application.Features.Commands.Users.CreateUser;

public record CreateUserCommand(
    string Name,
    string Email,
    decimal Balance,
    List<InitialWishlistItemDto> Wishlist
) : IRequest<Guid>;

public record InitialWishlistItemDto(
    string Title,
    string Platform
);