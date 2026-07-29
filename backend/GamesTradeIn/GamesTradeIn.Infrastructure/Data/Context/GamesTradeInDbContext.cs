using System.Data;
using GamesTradeIn.Domain.Aggregates.User;
using GamesTradeIn.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GamesTradeIn.Infrastructure.Data.Context;

public class GamesTradeInDbContext : DbContext
{
    public GamesTradeInDbContext(DbContextOptions<GamesTradeInDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
        
        public IDbConnection Connection => Database.GetDbConnection();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
}