using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Migrations
{
    /// <inheritdoc />
    public partial class removedIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardHolders_IdTypes_IdTypeId",
                table: "CardHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_CardHolders_HolderIdNumber",
                table: "CreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_IdTypes_IdTypeId",
                table: "CreditCard");

            migrationBuilder.DropTable(
                name: "IdTypes");

            migrationBuilder.DropIndex(
                name: "IX_CreditCard_HolderIdNumber",
                table: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_CreditCard_IdTypeId",
                table: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_CardHolders_IdTypeId",
                table: "CardHolders");

            migrationBuilder.DropColumn(
                name: "IdTypeId",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "IdTypeId",
                table: "CardHolders");

            migrationBuilder.AlterColumn<string>(
                name: "HolderIdNumber",
                table: "CreditCard",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "CardHolderModelIdNumber",
                table: "CreditCard",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_CardHolderModelIdNumber",
                table: "CreditCard",
                column: "CardHolderModelIdNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CardHolders_CardHolderModelIdNumber",
                table: "CreditCard",
                column: "CardHolderModelIdNumber",
                principalTable: "CardHolders",
                principalColumn: "IdNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCard_CardHolders_CardHolderModelIdNumber",
                table: "CreditCard");

            migrationBuilder.DropIndex(
                name: "IX_CreditCard_CardHolderModelIdNumber",
                table: "CreditCard");

            migrationBuilder.DropColumn(
                name: "CardHolderModelIdNumber",
                table: "CreditCard");

            migrationBuilder.AlterColumn<int>(
                name: "HolderIdNumber",
                table: "CreditCard",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "IdTypeId",
                table: "CreditCard",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeId",
                table: "CardHolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IdTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_HolderIdNumber",
                table: "CreditCard",
                column: "HolderIdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_IdTypeId",
                table: "CreditCard",
                column: "IdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardHolders_IdTypeId",
                table: "CardHolders",
                column: "IdTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardHolders_IdTypes_IdTypeId",
                table: "CardHolders",
                column: "IdTypeId",
                principalTable: "IdTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_CardHolders_HolderIdNumber",
                table: "CreditCard",
                column: "HolderIdNumber",
                principalTable: "CardHolders",
                principalColumn: "IdNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCard_IdTypes_IdTypeId",
                table: "CreditCard",
                column: "IdTypeId",
                principalTable: "IdTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
