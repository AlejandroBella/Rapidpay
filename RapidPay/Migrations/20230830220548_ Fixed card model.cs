using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Migrations
{
    /// <inheritdoc />
    public partial class Fixedcardmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balance_CreditCard_CardNumber",
                table: "Balance");

            migrationBuilder.DropIndex(
                name: "IX_Balance_CardNumber",
                table: "Balance");

            migrationBuilder.AddColumn<Guid>(
                name: "BalanceId",
                table: "CreditCard",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_BalanceId",
                table: "CreditCard",
                column: "BalanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_Balance_BalanceId",
                table: "CreditCard",
                column: "BalanceId",
                principalTable: "Balance",
                principalColumn: "BalanceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_Balance_BalanceId",
                table: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_CreditCard_BalanceId",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "CreditCard");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_CardNumber",
                table: "Balance",
                column: "CardNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Balance_CreditCard_CardNumber",
                table: "Balance",
                column: "CardNumber",
                principalTable: "CreditCard",
                principalColumn: "Number",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
