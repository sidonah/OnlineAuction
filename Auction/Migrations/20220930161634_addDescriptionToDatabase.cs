using Microsoft.EntityFrameworkCore.Migrations;

namespace Auction.Migrations
{
    public partial class addDescriptionToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lots",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");
        }
    }
}
