using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksAdmin.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_preco_atual_stocks_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "preco_atual",
                table: "stocks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preco_atual",
                table: "stocks");
        }
    }
}
