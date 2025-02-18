using Microsoft.Data.SqlClient;
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

            var result = await _context.BootcamperQuizzes.FindAsync(bq.BootcamperId, bq.QuizId);

            if(result == null)
            {
                var bootcamperQuiz = await _context.BootcamperQuizzes.AddAsync(bq);
                if (bootcamperQuiz == null)
                {
                    return null;
                }
                await _context.SaveChangesAsync();
                return bootcamperQuiz.Entity;
            }
            else
            {
                result.Score = bq.Score;
                await _context.SaveChangesAsync();
                return result;
            }

            
            
        }

        public async Task<ICollection<BootcamperQuiz>> GetAllBootcamperQuizByQuizId(int quizId)
        {
            var bootcamperQuiz = await _context.BootcamperQuizzes.Where(q => q.QuizId == quizId)
                .Include(q => q.Quizzes)
                .Include(b => b.Bootcampers)
                .ToListAsync();
            if(bootcamperQuiz == null)
            {
                return null;
            }
            return bootcamperQuiz;
        }

        public async Task<BootcamperQuiz> GetBootcamperQuizForBootcamper(int bootcamperId, int quizId)
        {
            var result = await _context.BootcamperQuizzes.FindAsync(bootcamperId, quizId);
            if(result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<ICollection<BootcamperQuiz>> GetBootcamperQuizForBootcamperAllQuiz(int bootcamperId)
        {
            var bootcamperQuiz = await _context.BootcamperQuizzes.Where(q => q.BootcamperId == bootcamperId)
                .Include(q => q.Quizzes)
                .Include(b => b.Bootcampers)
                .ToListAsync();
            if (bootcamperQuiz == null)
            {
                return null;
            }
            return bootcamperQuiz;
        }
    }
}
