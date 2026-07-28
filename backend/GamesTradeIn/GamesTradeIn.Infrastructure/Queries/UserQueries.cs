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
                                u."Id",
                                u."Name",
                                u."Balance" AS WalletBalance,
                                (SELECT COUNT(1) FROM "WishlistItems" WHERE "UserId" = u."Id")
                            FROM "Users" u
                            INNER JOIN "Wallets" w ON w."UserId" = u."Id"
                            WHERE u."Id" = @Id
                            """;

        return await connection.QueryFirstOrDefaultAsync<UserProfileDto>(sql, new { Id = id });
    }
}