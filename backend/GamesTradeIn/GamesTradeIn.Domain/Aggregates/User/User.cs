using GamesTradeIn.Domain.Exceptions;

namespace GamesTradeIn.Domain.Aggregates.User;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Wallet Wallet { get; private set; }
    private readonly List<WishListItem> _wishlist = [];
    public IReadOnlyCollection<WishListItem> WishList => _wishlist.AsReadOnly();

    private User() {}

    public User(Guid id, string name, string email, decimal initialBalance)
    {
        if(id == Guid.Empty)
            throw new DomainException("User Id cannot be empty");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("User Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("User Email cannot be empty");

        Id = id;
        Name = name;
        Email = email;
        Wallet = new Wallet(initialBalance);
    }

    public void AddToWishlist(string title, string platform)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Wishlist item title cannot be empty");
        
        if (string.IsNullOrWhiteSpace(platform))
            throw new DomainException("Wishlist item platform cannot be empty");
        
        if (_wishlist.Any(w => w.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && w.Platform.Equals(platform, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException("Wishlist item already exists");
        
        var item = new WishListItem(Guid.NewGuid(), title, platform);
        _wishlist.Add(item);
    }
}