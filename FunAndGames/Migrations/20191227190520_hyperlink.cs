using Microsoft.EntityFrameworkCore.Migrations;

namespace FunAndGames.Migrations
{
    public partial class hyperlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "linkURL",
                table: "UserPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "linkURL",
                table: "UserPosts");
        }
    }
}
