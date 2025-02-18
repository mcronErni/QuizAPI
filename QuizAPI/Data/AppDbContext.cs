using Microsoft.EntityFrameworkCore;
using QuizAPI.Model;

namespace QuizAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Bootcamper> Bootcampers { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<BootcamperQuiz> BootcamperQuizzes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BootcamperQuiz>()
                .HasKey(bq => new { bq.BootcamperId, bq.QuizId});
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                          .EnableSensitiveDataLogging();
        }

    }
}
