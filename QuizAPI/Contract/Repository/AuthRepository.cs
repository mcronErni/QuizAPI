using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Bootcamper> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Bootcamper> Register(Bootcamper bootcamper)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}
