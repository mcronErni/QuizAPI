using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class changed_clname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Mentor_MentorId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mentor",
                table: "Mentor");

            migrationBuilder.RenameTable(
                name: "Mentor",
                newName: "Mentors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mentors",
                table: "Mentors",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Mentors_MentorId",
                table: "Quizzes",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "MentorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Mentors_MentorId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mentors",
                table: "Mentors");

            migrationBuilder.RenameTable(
                name: "Mentors",
                newName: "Mentor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mentor",
                table: "Mentor",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Mentor_MentorId",
                table: "Quizzes",
                column: "MentorId",
                principalTable: "Mentor",
                principalColumn: "MentorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
