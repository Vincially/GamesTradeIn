using System.Runtime.InteropServices.JavaScript;

namespace GamesTradeIn.Domain.Aggregates.User;

public class WishListItem
{
    public Guid Id { get; init; }
    public string Title { get; set; } = null!;
    public string Platform { get; set; } = null!;
    public DateTime AddedAt { get; private set; }
    
    private WishListItem() {}
    
    public WishListItem(Guid id, string title, string platform)
    {
        Id = id;
        Title = title;
        Platform = platform;
        AddedAt = DateTime.UtcNow.Date;
    }
}