using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeDirectMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class TradeAndCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencySymbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencySymbol);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeTrades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ExchangeTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeTrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyExchangeTrade",
                columns: table => new
                {
                    CurrenciesCurrencySymbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExchangeTradesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchangeTrade", x => new { x.CurrenciesCurrencySymbol, x.ExchangeTradesId });
                    table.ForeignKey(
                        name: "FK_CurrencyExchangeTrade_Currencies_CurrenciesCurrencySymbol",
                        column: x => x.CurrenciesCurrencySymbol,
                        principalTable: "Currencies",
                        principalColumn: "CurrencySymbol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyExchangeTrade_ExchangeTrades_ExchangeTradesId",
                        column: x => x.ExchangeTradesId,
                        principalTable: "ExchangeTrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyExchangeTrade_ExchangeTradesId",
                table: "CurrencyExchangeTrade",
                column: "ExchangeTradesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyExchangeTrade");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "ExchangeTrades");
        }
    }
}
