using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class deneme12312311 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "Post",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isactive",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Post");
        }
    }
}
