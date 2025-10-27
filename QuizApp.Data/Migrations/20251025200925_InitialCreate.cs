using System;
using Microsoft.EntityFrameworkCore.Migrations; 

#nullable disable

namespace QuizApp.Data.Migrations
{
    // denne midigreringen bygger databasen
    //vi brukte AI på delene som gjelder fremednøklene, ettersom vi slet med å få til dette fra bunnen av
    public partial class InitialCreate : Migration
    {

        // metoden Up kjøres når vi skal bygge eller oppdatere databasen til en nyre versjon
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // forsøk på å ta en quiz
            migrationBuilder.CreateTable(
        name: "Attempts",
        columns: table => new
        {

            Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),

            QuizId = table.Column<int>(type: "INTEGER", nullable: false),

            TakenUtc = table.Column<DateTime>(type: "TEXT", nullable: false),

            TotalScore = table.Column<int>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {

            table.PrimaryKey("PK_Attempts", x => x.Id);
        });

            //selve quizzen
            migrationBuilder.CreateTable(
        name: "Quizzes",
        columns: table => new
        {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),

            Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),

            Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),

            CreatedUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Quizzes", x => x.Id);
        });

            //svar på quizzen
            migrationBuilder.CreateTable(
        name: "Answers",
        columns: table => new
        {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),

            QuizAttemptId = table.Column<int>(type: "INTEGER", nullable: false),

            QuestionId = table.Column<int>(type: "INTEGER", nullable: false),

            SelectedOptionIdsCsv = table.Column<string>(type: "TEXT", nullable: false),

            EarnedPoints = table.Column<int>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Answers", x => x.Id);

            table.ForeignKey(
              name: "FK_Answers_Attempts_QuizAttemptId",
              column: x => x.QuizAttemptId,
              principalTable: "Attempts",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);
        });

            // spørsmålene i en quiz
            migrationBuilder.CreateTable(
        name: "Questions",
        columns: table => new
        {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),
            QuizId = table.Column<int>(type: "INTEGER", nullable: false),
            Text = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
            Points = table.Column<int>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Questions", x => x.Id);

            table.ForeignKey(
              name: "FK_Questions_Quizzes_QuizId",
              column: x => x.QuizId,
              principalTable: "Quizzes",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);
        });

            // valgmuligheter
            migrationBuilder.CreateTable(
        name: "Options",
        columns: table => new
        {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
            .Annotation("Sqlite:Autoincrement", true),
            QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
            Text = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
            IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Options", x => x.Id);

            table.ForeignKey(
              name: "FK_Options_Questions_QuestionId",
              column: x => x.QuestionId,
              principalTable: "Questions",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);
        });

            //indekser. Dette brukes for å rask gunne finne svar, spørsmål og alternativer
            migrationBuilder.CreateIndex(
        name: "IX_Answers_QuizAttemptId",
        table: "Answers",
        column: "QuizAttemptId");

            migrationBuilder.CreateIndex(
              name: "IX_Options_QuestionId",
              table: "Options",
              column: "QuestionId");

            migrationBuilder.CreateIndex(
              name: "IX_Questions_QuizId",
              table: "Questions",
              column: "QuizId");
        }

        
        // Metoden Down kjøres når man ruller tilbake databasen til en tidligere versjon. AI anbefalte å ha med dette, selv da 
        // det ikke er en nødvendig funksjon for en MVP
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // slette tabellene i riktig rekkefølge. Altså først barn, så foreldre
            migrationBuilder.DropTable(
                name: "Answers"); 

            migrationBuilder.DropTable(
                name: "Options"); 

            migrationBuilder.DropTable(
                name: "Attempts"); 

            migrationBuilder.DropTable(
                name: "Questions"); 

            migrationBuilder.DropTable(
                name: "Quizzes"); 
        }
    }
}
