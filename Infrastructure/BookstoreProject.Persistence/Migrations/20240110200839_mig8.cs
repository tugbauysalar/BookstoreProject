using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Products");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Products",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
