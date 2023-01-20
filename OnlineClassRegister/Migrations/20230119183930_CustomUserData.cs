using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClassRegister.Migrations
{
    public partial class CustomUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_Studentid",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Subject_Subjectid",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_TeacherGradingid",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentClass_studentClassid",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_StudentClass_classTutoringId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_classTutoringId",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "studentClassid",
                table: "Student",
                newName: "studentClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_studentClassid",
                table: "Student",
                newName: "IX_Student_studentClassId");

            migrationBuilder.RenameColumn(
                name: "TeacherGradingid",
                table: "Grade",
                newName: "teacherGradingId");

            migrationBuilder.RenameColumn(
                name: "Subjectid",
                table: "Grade",
                newName: "subjectId");

            migrationBuilder.RenameColumn(
                name: "Studentid",
                table: "Grade",
                newName: "studentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_TeacherGradingid",
                table: "Grade",
                newName: "IX_Grade_teacherGradingId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_Subjectid",
                table: "Grade",
                newName: "IX_Grade_subjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_Studentid",
                table: "Grade",
                newName: "IX_Grade_studentId");

            migrationBuilder.AlterColumn<int>(
                name: "classTutoringId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "studentClassId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityOfBirth",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_classTutoringId",
                table: "Teacher",
                column: "classTutoringId",
                unique: true,
                filter: "[classTutoringId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_studentId",
                table: "Grade",
                column: "studentId",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Subject_subjectId",
                table: "Grade",
                column: "subjectId",
                principalTable: "Subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_teacherGradingId",
                table: "Grade",
                column: "teacherGradingId",
                principalTable: "Teacher",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentClass_studentClassId",
                table: "Student",
                column: "studentClassId",
                principalTable: "StudentClass",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_StudentClass_classTutoringId",
                table: "Teacher",
                column: "classTutoringId",
                principalTable: "StudentClass",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_studentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Subject_subjectId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Teacher_teacherGradingId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentClass_studentClassId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_StudentClass_classTutoringId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_classTutoringId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "CityOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "studentClassId",
                table: "Student",
                newName: "studentClassid");

            migrationBuilder.RenameIndex(
                name: "IX_Student_studentClassId",
                table: "Student",
                newName: "IX_Student_studentClassid");

            migrationBuilder.RenameColumn(
                name: "teacherGradingId",
                table: "Grade",
                newName: "TeacherGradingid");

            migrationBuilder.RenameColumn(
                name: "subjectId",
                table: "Grade",
                newName: "Subjectid");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Grade",
                newName: "Studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_teacherGradingId",
                table: "Grade",
                newName: "IX_Grade_TeacherGradingid");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_subjectId",
                table: "Grade",
                newName: "IX_Grade_Subjectid");

            migrationBuilder.RenameIndex(
                name: "IX_Grade_studentId",
                table: "Grade",
                newName: "IX_Grade_Studentid");

            migrationBuilder.AlterColumn<int>(
                name: "classTutoringId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "studentClassid",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_classTutoringId",
                table: "Teacher",
                column: "classTutoringId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_Studentid",
                table: "Grade",
                column: "Studentid",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Subject_Subjectid",
                table: "Grade",
                column: "Subjectid",
                principalTable: "Subject",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Teacher_TeacherGradingid",
                table: "Grade",
                column: "TeacherGradingid",
                principalTable: "Teacher",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentClass_studentClassid",
                table: "Student",
                column: "studentClassid",
                principalTable: "StudentClass",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_StudentClass_classTutoringId",
                table: "Teacher",
                column: "classTutoringId",
                principalTable: "StudentClass",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
