using GamesTradeIn.Domain.Aggregates.User;

namespace GamesTradeIn.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(string email);
    Task SaveChangesAsync();
}