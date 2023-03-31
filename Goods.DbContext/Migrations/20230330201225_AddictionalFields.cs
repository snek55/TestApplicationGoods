using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goods.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddictionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomFields",
                table: "Products",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "AdditionalFields",
                table: "Categories",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomFields",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AdditionalFields",
                table: "Categories");
        }
    }
}
