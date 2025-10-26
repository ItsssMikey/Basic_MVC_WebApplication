using Microsoft.EntityFrameworkCore;
using QuizApp.Domain;

namespace QuizApp.Data
{
    //dette er selve database-registeret for applikasjonen

    public class AppDbContext : DbContext
    {
        //konstruktør
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//tabellene i databasen
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Option> Options { get; set; } = null!;
        
//denne metoden kjører når EF core bygger sin model av databasen
        protected override void OnModelCreating(ModelBuilder b)
        {

            //disse to delene konfigurerer lenker mellom questions og quiz, og option og question
            b.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(z => z.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
