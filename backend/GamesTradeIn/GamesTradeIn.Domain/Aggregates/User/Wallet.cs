namespace GamesTradeIn.Domain.Aggregates.User;

public class Wallet
{
    private Wallet() {}
    
    public Wallet(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public decimal Balance { get; private set; }
}