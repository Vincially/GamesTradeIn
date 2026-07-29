namespace GamesTradeIn.Application.Features.Queries.GetUserProfile;

public record UserProfileDto(
    Guid Id,
    string Name,
    string Email,
    decimal Balance,
    List<WishlistItemDto> Wishlist
);

public record WishlistItemDto(
    Guid Id,
    string Title,
    string Platform
);
