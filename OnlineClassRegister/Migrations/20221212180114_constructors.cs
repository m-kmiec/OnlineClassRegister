using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class constructors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Teacher_Teacherid",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_Teacherid",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Teacherid",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    subjectsid = table.Column<int>(type: "int", nullable: false),
                    teachersid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.subjectsid, x.teachersid });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subject_subjectsid",
                        column: x => x.subjectsid,
                        principalTable: "Subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Teacher_teachersid",
                        column: x => x.teachersid,
                        principalTable: "Teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_teachersid",
                table: "SubjectTeacher",
                column: "teachersid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.AddColumn<int>(
                name: "Teacherid",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Teacherid",
                table: "Subject",
                column: "Teacherid");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Teacher_Teacherid",
                table: "Subject",
                column: "Teacherid",
                principalTable: "Teacher",
                principalColumn: "id");
        }
    }
}
