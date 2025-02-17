using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRepository(AppDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Account> Register(Account account, string password)
        {
            if (await UserExists(account.Username, account.Email))
                return null;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<Account> Login(string usernameOrEmail, string password)
        {
            // Fetch the Account first
            var account = await _context.Accounts
                .FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);

            if (account == null)
            {
                return null;
            }

            // Based on the role, conditionally load the related entity
            if (account.Role == "bootcamper")
            {
                // Load Bootcamper details if the role is "bootcamper"
                await _context.Entry(account)
                    .Reference(a => a.Bootcamper)
                    .LoadAsync(); // This will load the Bootcamper entity
            }
            else if (account.Role == "mentor")
            {
                // Load Mentor details if the role is "mentor"
                await _context.Entry(account)
                    .Reference(a => a.Mentor)
                    .LoadAsync(); // This will load the Mentor entity
            }

            if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
                return null;

            return account;
        }

        public async Task Logout()
        {
            var response = _httpContextAccessor.HttpContext.Response;

            // Remove the JWT token and related cookies
            response.Cookies.Delete("jwt");
            response.Cookies.Delete("role");
            response.Cookies.Delete("mentorId");
            response.Cookies.Delete("bootcamperId");

            await Task.CompletedTask;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            if (await _context.Accounts.AnyAsync(x => x.Username == username))
                return true;
            if (await _context.Accounts.AnyAsync(x => x.Email == email))
                return true;
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }

}
