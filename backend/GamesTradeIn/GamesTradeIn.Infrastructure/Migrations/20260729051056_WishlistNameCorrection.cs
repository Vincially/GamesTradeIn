using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesTradeIn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WishlistNameCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Users_User",
                table: "WishList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishList",
                table: "WishList");

            migrationBuilder.RenameTable(
                name: "WishList",
                newName: "Wishlist");

            migrationBuilder.RenameIndex(
                name: "IX_WishList_User",
                table: "Wishlist",
                newName: "IX_Wishlist_User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Users_User",
                table: "Wishlist",
                column: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Users_User",
                table: "Wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlist",
                table: "Wishlist");

            migrationBuilder.RenameTable(
                name: "Wishlist",
                newName: "WishList");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlist_User",
                table: "WishList",
                newName: "IX_WishList_User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishList",
                table: "WishList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Users_User",
                table: "WishList",
                column: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
