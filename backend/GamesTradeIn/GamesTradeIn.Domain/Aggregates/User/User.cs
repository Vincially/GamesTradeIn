namespace GamesTradeIn.Domain.Aggregates.UserAggregates;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Wallet Wallet { get; set; }
    public List<WishListItem> WishList { get; set; }
    
}