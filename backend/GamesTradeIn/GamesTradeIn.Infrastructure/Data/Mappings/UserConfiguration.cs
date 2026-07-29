using GamesTradeIn.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamesTradeIn.Infrastructure.Data.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(user => user.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.OwnsOne(user => user.Wallet, walletBuilder =>
        {
            walletBuilder
                .Property(user => user.Balance)
                .HasColumnName("WalletBalance")
                .HasPrecision(18, 2)
                .IsRequired();
        });

        builder.OwnsMany(user => user.WishList, wishListBuilder =>
        {
            wishListBuilder.ToTable("WishList");
            wishListBuilder.HasKey(wishList => wishList.Id);
            wishListBuilder.Property(w => w.Title)
                .HasMaxLength(250)
                .IsRequired();
            wishListBuilder.Property(w => w.Platform)
                .HasMaxLength(100)
                .IsRequired();
            wishListBuilder.WithOwner()
                .HasForeignKey("User");
        });
    }
}