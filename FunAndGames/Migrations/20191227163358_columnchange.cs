using Microsoft.EntityFrameworkCore.Migrations;

namespace FunAndGames.Migrations
{
    public partial class columnchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "linkPath",
                table: "UserPosts");

            migrationBuilder.AddColumn<string>(
                name: "linkImagePath",
                table: "UserPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "linkImagePath",
                table: "UserPosts");

            migrationBuilder.AddColumn<string>(
                name: "linkPath",
                table: "UserPosts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
