using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class Accounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MentorId",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BootcamperEmail",
                table: "Bootcampers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Bootcampers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Bootcampers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    MentorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MentorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor", x => x.MentorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_MentorId",
                table: "Quizzes",
                column: "MentorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Mentor_MentorId",
                table: "Quizzes",
                column: "MentorId",
                principalTable: "Mentor",
                principalColumn: "MentorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Mentor_MentorId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_MentorId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "MentorId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "BootcamperEmail",
                table: "Bootcampers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Bootcampers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Bootcampers");
        }
    }
}
