using System.Data;
using GamesTradeIn.Domain.Aggregates.UserAggregates;
using Microsoft.EntityFrameworkCore;

namespace GamesTradeIn.Infrastructure.Data.Context;

public class GamesTradeInDbContext : DbContext
{
    public GamesTradeInDbContext(DbContextOptions<GamesTradeInDbContext> options) : base(options) { }
    
        public DbSet<User> Users => Set<User>();
        
        public IDbConnection Connection => Database.GetDbConnection();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamesTradeInDbContext).Assembly);
        }
    
}