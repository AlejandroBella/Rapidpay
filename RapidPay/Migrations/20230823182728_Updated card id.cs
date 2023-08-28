using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Migrations
{
    /// <inheritdoc />
    public partial class Updatedcardid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_IdTypeId",
                table: "CreditCard",
                column: "IdTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_CreditCard_IdTypeId",
                table: "CreditCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCard",
                table: "CreditCard",
                columns: new[] { "IdTypeId", "Number" });
        }
    }
}
