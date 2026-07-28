using System.Data;
using GamesTradeIn.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;

namespace GamesTradeIn.Infrastructure.Data.Context;

public class GamesTradeInDbContext(DbContextOptions<GamesTradeInDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
        
        public IDbConnection Connection => Database.GetDbConnection();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamesTradeInDbContext).Assembly);
        }
    
}