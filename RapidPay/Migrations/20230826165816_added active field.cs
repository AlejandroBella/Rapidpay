using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Migrations
{
    /// <inheritdoc />
    public partial class addedactivefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "CreditCard",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "CardHolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "CardHolders");
        }
    }
}
