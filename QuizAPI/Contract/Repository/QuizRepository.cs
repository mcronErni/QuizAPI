using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Quiz?> CreateQuiz(Quiz quiz)
        {
            var created_quiz = await _context.Quizzes.AddAsync(quiz);
            if (created_quiz == null) { return null; }
            await _context.SaveChangesAsync();
            return quiz;

        }

        public async Task<Quiz?> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == id);
            if (quiz == null) { return null; }
            _context.Remove(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<ICollection<Quiz>> Get()
        {
            var quizzes = await _context.Quizzes
                .Include(m => m.Mentor)
                .ToListAsync();
            return quizzes;
        }

        public async Task<Quiz>? GetById(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId.Equals(id));
            if(quiz == null)
            {
                return null;
            }
            return quiz;
        }

        public Task<Quiz>? UpdateQuiz(int id, Quiz quiz)
        {
            throw new NotImplementedException();
        }
    }
}
