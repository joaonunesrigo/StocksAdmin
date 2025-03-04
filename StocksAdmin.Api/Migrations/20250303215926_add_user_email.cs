using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksAdmin.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_user_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "users");
        }
    }
}
