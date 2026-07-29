using Dapper;
using GamesTradeIn.Application.Features.Queries.GetUserProfile;
using GamesTradeIn.Domain.Repositories;
using GamesTradeIn.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GamesTradeIn.Infrastructure.Queries;

public class UserQueries(GamesTradeInDbContext context) : IUserQueries
{
    public async Task<UserProfileDto?> GetUserProfile(Guid id)
    {
        var connection = context.Database.GetDbConnection();

        const string sql = """
                            SELECT 
                                "Id", 
                                "Name", 
                                "Email", 
                                "WalletBalance"
                            FROM "Users"
                            WHERE "Id" = @Id;

                            SELECT 
                                "Id",
                                "Title",
                                "Platform"
                                FROM "Wishlists" 
                                WHERE "UserId" = @Id;
                            """;

        using var multi = await connection.QueryMultipleAsync(sql, new {Id = id});

        var user = await multi.ReadFirstOrDefaultAsync();

        if (user is null)
            return null;
        
        var wishlist = (await multi.ReadAsync<WishlistItemDto>()).ToList();

        return new UserProfileDto(
            (Guid)user.Id,
            (string)user.Name,
            (string)user.Email,
            (decimal)user.WalletBalance,
            wishlist);
    }
}