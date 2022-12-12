using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherGradingid = table.Column<int>(type: "int", nullable: false),
                    Studentid = table.Column<int>(type: "int", nullable: false),
                    Subjectid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_Student_Studentid",
                        column: x => x.Studentid,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grade_Subject_Subjectid",
                        column: x => x.Subjectid,
                        principalTable: "Subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grade_Teacher_TeacherGradingid",
                        column: x => x.TeacherGradingid,
                        principalTable: "Teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Studentid",
                table: "Grade",
                column: "Studentid");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_Subjectid",
                table: "Grade",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_TeacherGradingid",
                table: "Grade",
                column: "TeacherGradingid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");
        }
    }
}
