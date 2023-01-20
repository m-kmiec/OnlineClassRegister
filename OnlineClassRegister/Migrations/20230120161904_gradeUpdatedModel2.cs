using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class gradeUpdatedModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "semesterNumber",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "semesterNumber",
                table: "Grade");
        }
    }
}
