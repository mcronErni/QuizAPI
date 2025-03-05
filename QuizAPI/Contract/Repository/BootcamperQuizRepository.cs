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
        public async Task<BootcamperQuiz> CreateBootcamperQuiz(BootcamperQuiz input)
        {
            //var bootcamperQuiz = await _context.BootcamperQuizzes
            //    //.Include(q => q.Quiz)
            //    //.Include(b => b.Bootcampers)
            //    .Where(bq => bq.BootcamperId == input.BootcamperId && bq.QuizId == input.QuizId).FirstOrDefaultAsync();
            //.AsQueryable();

            var bootcamper = await _context.Bootcampers.FirstOrDefaultAsync(bc => bc.Id == input.BootcamperId);
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == input.QuizId);

            if (bootcamper == null || quiz == null)
            {
                return null;
            }

            var result = await _context.BootcamperQuizzes.FindAsync(input.BootcamperId, input.QuizId);

            //bootcamperQuiz = bootcamperQuiz.Where(bq => bq.BootcamperId == input.BootcamperId && bq.QuizId == input.QuizId);

            if(result == null)
            {
                await _context.BootcamperQuizzes.AddAsync(input);
                await _context.SaveChangesAsync();
                //if (bootcamperQuiz == null)
                //{
                //    return null;
                //}
                //return bootcamperQuiz.Entity;
            }
                result.Score = input.Score;
                await _context.SaveChangesAsync();
                return result;
            

            
            
        }

        public async Task<ICollection<BootcamperQuiz>> GetAllBootcamperQuizByQuizId(int quizId)
        {
            var bootcamperQuiz = await _context.BootcamperQuizzes.Where(q => q.QuizId == quizId)
                .Include(q => q.Quiz)
                .Include(b => b.Bootcampers)
                .Where(q => q.Quiz.IsDeleted == false)
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
                .Include(q => q.Quiz)
                .Include(b => b.Bootcampers)
                .Where(q => q.Quiz.IsDeleted == false)
                .ToListAsync();
            if (bootcamperQuiz == null)
            {
                return null;
            }
            return bootcamperQuiz;
        }
    }
}
