using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class StudentModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentClass_studentClassid",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "studentClassid",
                table: "Student",
                newName: "studentClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_studentClassid",
                table: "Student",
                newName: "IX_Student_studentClassId");

            migrationBuilder.AlterColumn<int>(
                name: "studentClassId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentClass_studentClassId",
                table: "Student",
                column: "studentClassId",
                principalTable: "StudentClass",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentClass_studentClassId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "studentClassId",
                table: "Student",
                newName: "studentClassid");

            migrationBuilder.RenameIndex(
                name: "IX_Student_studentClassId",
                table: "Student",
                newName: "IX_Student_studentClassid");

            migrationBuilder.AlterColumn<int>(
                name: "studentClassid",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentClass_studentClassid",
                table: "Student",
                column: "studentClassid",
                principalTable: "StudentClass",
                principalColumn: "id");
        }
    }
}
