using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class gradeUpdatedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "value",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "Grade");
        }
    }
}
