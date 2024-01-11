using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Products",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
