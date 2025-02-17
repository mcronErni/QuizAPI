using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class MentorRepository : IMentorRepository
    {
        private readonly AppDbContext _context;

        public MentorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Mentor?> CreateMentor(Mentor mentor)
        {
            try
            {
                var created_mentor = await _context.Mentors.AddAsync(mentor);
                if (created_mentor == null)
                {
                    return null;
                }
                await _context.SaveChangesAsync();
                return mentor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong while adding the data");
            }
        }

        public async Task<Mentor?> DeleteMentor(int id)
        {
            var mentor = await _context.Mentors
                .FirstOrDefaultAsync(bc => bc.MentorId == id);
            if (mentor == null)
            {
                return null;
            }
            _context.Remove(mentor);
            await _context.SaveChangesAsync();
            return mentor;
        }

        public async Task<ICollection<Mentor>?> Get()
        {
            var mentors = await _context.Mentors.ToListAsync();
            return mentors;
        }

        public async Task<Mentor?> GetById(int id)
        {
            var mentor = await _context.Mentors
                .Include(q => q.Quizzes)
                .FirstOrDefaultAsync(bc => bc.MentorId == id);
            if (mentor == null)
            {
                return null;
            }
            return mentor;
        }

        public Task<Mentor?> UpdateMentor(int id, Mentor mentor)
        {
            throw new NotImplementedException();
        }
    }
}
