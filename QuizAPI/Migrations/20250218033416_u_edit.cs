using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizAPI.Migrations
{
    /// <inheritdoc />
    public partial class u_edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bootcampers",
                columns: table => new
                {
                    BootcamperId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bootcampers", x => x.BootcamperId);
                });

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    MentorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.MentorId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BootcamperId = table.Column<int>(type: "int", nullable: true),
                    MentorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Bootcampers_BootcamperId",
                        column: x => x.BootcamperId,
                        principalTable: "Bootcampers",
                        principalColumn: "BootcamperId");
                    table.ForeignKey(
                        name: "FK_Accounts_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "MentorId");
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    MentorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quizzes_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "MentorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BootcamperQuizzes",
                columns: table => new
                {
                    BootcamperId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BootcamperQuizzes", x => new { x.BootcamperId, x.QuizId });
                    table.ForeignKey(
                        name: "FK_BootcamperQuizzes_Bootcampers_BootcamperId",
                        column: x => x.BootcamperId,
                        principalTable: "Bootcampers",
                        principalColumn: "BootcamperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BootcamperQuizzes_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BootcamperId",
                table: "Accounts",
                column: "BootcamperId",
                unique: true,
                filter: "[BootcamperId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_MentorId",
                table: "Accounts",
                column: "MentorId",
                unique: true,
                filter: "[MentorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BootcamperQuizzes_QuizId",
                table: "BootcamperQuizzes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_MentorId",
                table: "Quizzes",
                column: "MentorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "BootcamperQuizzes");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Bootcampers");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Mentors");
        }
    }
}
