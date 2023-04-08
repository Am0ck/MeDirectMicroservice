using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeDirectMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class TradeAndCurrency2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ExchangeTrades",
                newName: "TradedAmount");

            migrationBuilder.AddColumn<double>(
                name: "AmountToTrade",
                table: "ExchangeTrades",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToTrade",
                table: "ExchangeTrades");

            migrationBuilder.RenameColumn(
                name: "TradedAmount",
                table: "ExchangeTrades",
                newName: "Amount");
        }
    }
}
