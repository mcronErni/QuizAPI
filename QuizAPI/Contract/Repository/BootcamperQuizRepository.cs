using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class BootcamperQuizRepository : IBootcamperQuizRepository
    {
        private readonly AppDbContext _context;

        public BootcamperQuizRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BootcamperQuiz> CreateBootcamperQuiz(BootcamperQuiz bq)
        {
            var bootcamper = await _context.Bootcampers.FirstOrDefaultAsync(bc => bc.BootcamperId == bq.BootcamperId);
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == bq.QuizId);

            if (bootcamper == null || quiz == null)
            {
                return null;
            }

            var bootcamperQuiz = await _context.BootcamperQuizzes.AddAsync(bq);
            if(bootcamperQuiz == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return bootcamperQuiz.Entity;
        }

        public async Task<ICollection<BootcamperQuiz>> GetAllBootcamperQuiz()
        {
            var bootcamperQuiz = await _context.BootcamperQuizzes.ToListAsync();
            return bootcamperQuiz;
        }

        public async Task<BootcamperQuiz> GetBootcamperQuiz(int bootcamperId, int quizId)
        {
            var result = await _context.BootcamperQuizzes.FindAsync(bootcamperId, quizId);
            if(result == null)
            {
                return null;
            }
            return result;
        }
    }
}
