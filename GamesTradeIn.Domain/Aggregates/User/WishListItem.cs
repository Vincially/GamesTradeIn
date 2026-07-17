namespace GamesTradeIn.Domain.Aggregates.UserAggregates;

public class WishListItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Platform { get; set; }
    public DateTime AddedAt { get; set; }
}