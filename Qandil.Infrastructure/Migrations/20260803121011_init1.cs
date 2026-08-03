using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestName",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PostTestResilt",
                table: "Childs");

            migrationBuilder.DropColumn(
                name: "PreEduTestResult",
                table: "Childs");

            migrationBuilder.RenameColumn(
                name: "TestType",
                table: "Tests",
                newName: "TotalMark");

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "Tests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Tests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ChildTests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "ChildTests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Mark",
                table: "ChildTests",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Nots",
                table: "ChildTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ChildTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LevelId",
                table: "Tests",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildTests_EmployeeId",
                table: "ChildTests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildTests_Employees_EmployeeId",
                table: "ChildTests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Levels_LevelId",
                table: "Tests",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Subject_SubjectId",
                table: "Tests",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildTests_Employees_EmployeeId",
                table: "ChildTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Levels_LevelId",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subject_SubjectId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LevelId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SubjectId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_ChildTests_EmployeeId",
                table: "ChildTests");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ChildTests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ChildTests");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "ChildTests");

            migrationBuilder.DropColumn(
                name: "Nots",
                table: "ChildTests");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ChildTests");

            migrationBuilder.RenameColumn(
                name: "TotalMark",
                table: "Tests",
                newName: "TestType");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PostTestResilt",
                table: "Childs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreEduTestResult",
                table: "Childs",
                type: "int",
                nullable: true);
        }
    }
}
