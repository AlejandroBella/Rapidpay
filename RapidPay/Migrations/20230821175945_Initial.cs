using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidPay.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "CardHolders",
                columns: table => new
                {
                    IdNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardHolders", x => x.IdNumber);
                    table.ForeignKey(
                        name: "FK_CardHolders_IdTypes_IdTypeId",
                        column: x => x.IdTypeId,
                        principalTable: "IdTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    IdTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PIN = table.Column<int>(type: "INTEGER", nullable: false),
                    HolderIdNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastUpdate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => new { x.IdTypeId, x.Number });
                    table.ForeignKey(
                        name: "FK_CreditCard_CardHolders_HolderIdNumber",
                        column: x => x.HolderIdNumber,
                        principalTable: "CardHolders",
                        principalColumn: "IdNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCard_IdTypes_IdTypeId",
                        column: x => x.IdTypeId,
                        principalTable: "IdTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardHolders_IdTypeId",
                table: "CardHolders",
                column: "IdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_HolderIdNumber",
                table: "CreditCard",
                column: "HolderIdNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "CardHolders");

            migrationBuilder.DropTable(
                name: "IdTypes");
        }
    }
}
