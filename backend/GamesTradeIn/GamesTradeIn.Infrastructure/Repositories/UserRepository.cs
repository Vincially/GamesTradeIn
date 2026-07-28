using GamesTradeIn.Domain.Aggregates.User;
using GamesTradeIn.Domain.Repositories;
using GamesTradeIn.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GamesTradeIn.Infrastructure.Repositories;

public class UserRepository(GamesTradeInDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var result = await context.Users
            .Include(u => u.Wallet)
            .Include(u => u.WishList)
            .FirstOrDefaultAsync(u => u.Id == id);
        return result;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        var exists = await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        return exists;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}