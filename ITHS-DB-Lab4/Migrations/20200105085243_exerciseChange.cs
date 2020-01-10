using Microsoft.EntityFrameworkCore.Migrations;

namespace ITHS_DB_Lab4.Migrations
{
    public partial class exerciseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repetiotions",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "Repetiotions",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
