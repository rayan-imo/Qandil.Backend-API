using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildTestSubjectMarks_ChildTests_ChildTestId",
                table: "ChildTestSubjectMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildTestSubjectMarks_Employees_EmployeeId",
                table: "ChildTestSubjectMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildTestSubjectMarks_Subject_SubjectId",
                table: "ChildTestSubjectMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildTestSubjectMarks_TestSubjects_TestSubjectId",
                table: "ChildTestSubjectMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOption_DiagnosisQuestions_DiagnosisQuestionId",
                table: "QuestionOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionOption",
                table: "QuestionOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildTestSubjectMarks",
                table: "ChildTestSubjectMarks");

            migrationBuilder.RenameTable(
                name: "QuestionOption",
                newName: "QuestionOptions");

            migrationBuilder.RenameTable(
                name: "ChildTestSubjectMarks",
                newName: "SubjectMark");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionOption_DiagnosisQuestionId",
                table: "QuestionOptions",
                newName: "IX_QuestionOptions_DiagnosisQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTestSubjectMarks_TestSubjectId",
                table: "SubjectMark",
                newName: "IX_SubjectMark_TestSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTestSubjectMarks_SubjectId",
                table: "SubjectMark",
                newName: "IX_SubjectMark_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTestSubjectMarks_EmployeeId",
                table: "SubjectMark",
                newName: "IX_SubjectMark_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildTestSubjectMarks_ChildTestId",
                table: "SubjectMark",
                newName: "IX_SubjectMark_ChildTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectMark",
                table: "SubjectMark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_DiagnosisQuestions_DiagnosisQuestionId",
                table: "QuestionOptions",
                column: "DiagnosisQuestionId",
                principalTable: "DiagnosisQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectMark_ChildTests_ChildTestId",
                table: "SubjectMark",
                column: "ChildTestId",
                principalTable: "ChildTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectMark_Employees_EmployeeId",
                table: "SubjectMark",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectMark_Subject_SubjectId",
                table: "SubjectMark",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectMark_TestSubjects_TestSubjectId",
                table: "SubjectMark",
                column: "TestSubjectId",
                principalTable: "TestSubjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_DiagnosisQuestions_DiagnosisQuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectMark_ChildTests_ChildTestId",
                table: "SubjectMark");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectMark_Employees_EmployeeId",
                table: "SubjectMark");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectMark_Subject_SubjectId",
                table: "SubjectMark");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectMark_TestSubjects_TestSubjectId",
                table: "SubjectMark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectMark",
                table: "SubjectMark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions");

            migrationBuilder.RenameTable(
                name: "SubjectMark",
                newName: "ChildTestSubjectMarks");

            migrationBuilder.RenameTable(
                name: "QuestionOptions",
                newName: "QuestionOption");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectMark_TestSubjectId",
                table: "ChildTestSubjectMarks",
                newName: "IX_ChildTestSubjectMarks_TestSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectMark_SubjectId",
                table: "ChildTestSubjectMarks",
                newName: "IX_ChildTestSubjectMarks_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectMark_EmployeeId",
                table: "ChildTestSubjectMarks",
                newName: "IX_ChildTestSubjectMarks_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectMark_ChildTestId",
                table: "ChildTestSubjectMarks",
                newName: "IX_ChildTestSubjectMarks_ChildTestId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionOptions_DiagnosisQuestionId",
                table: "QuestionOption",
                newName: "IX_QuestionOption_DiagnosisQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildTestSubjectMarks",
                table: "ChildTestSubjectMarks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionOption",
                table: "QuestionOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTestSubjectMarks_ChildTests_ChildTestId",
                table: "ChildTestSubjectMarks",
                column: "ChildTestId",
                principalTable: "ChildTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTestSubjectMarks_Employees_EmployeeId",
                table: "ChildTestSubjectMarks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTestSubjectMarks_Subject_SubjectId",
                table: "ChildTestSubjectMarks",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTestSubjectMarks_TestSubjects_TestSubjectId",
                table: "ChildTestSubjectMarks",
                column: "TestSubjectId",
                principalTable: "TestSubjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOption_DiagnosisQuestions_DiagnosisQuestionId",
                table: "QuestionOption",
                column: "DiagnosisQuestionId",
                principalTable: "DiagnosisQuestions",
                principalColumn: "Id");
        }
    }
}
