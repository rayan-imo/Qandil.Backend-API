using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qandil.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "CardName",
                table: "DiagnosisQuestions");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "DiagnosisQuestions");

            migrationBuilder.AddColumn<int>(
                name: "CardType",
                table: "DiagnosisQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "DiagnosisQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "DiagnosisQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScoreInputType",
                table: "DiagnosisQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DiagnosisQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOption_DiagnosisQuestions_DiagnosisQuestionId",
                        column: x => x.DiagnosisQuestionId,
                        principalTable: "DiagnosisQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_DiagnosisQuestionId",
                table: "QuestionOption",
                column: "DiagnosisQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOption");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "DiagnosisQuestions");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "DiagnosisQuestions");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "DiagnosisQuestions");

            migrationBuilder.DropColumn(
                name: "ScoreInputType",
                table: "DiagnosisQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "DiagnosisQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "DiagnosisQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EvaluationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainTitleScoresJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCards_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCards_DiagnosisId",
                table: "EvaluationCards",
                column: "DiagnosisId");
        }
    }
}
