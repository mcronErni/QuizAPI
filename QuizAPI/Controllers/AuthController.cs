using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.Contract.DTO;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IBootcamperRepository _bootcamperRepository;
        private readonly IMentorRepository _mentorRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository,
                              IBootcamperRepository bootcamperRepository,
                              IMentorRepository mentorRepository,
                              IConfiguration config,
                              AppDbContext context,
                              IMapper mapper)
        {
            _authRepository = authRepository;
            _bootcamperRepository = bootcamperRepository;
            _mentorRepository = mentorRepository;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterAccountDTO account)
        {
            // Create the account first
            var accountToCreate = _mapper.Map<Account>(account);

            var createdAccount = await _authRepository.Register(accountToCreate, account.Password);

            if (createdAccount == null)
                return BadRequest("Username already exists");

            // Now create the Bootcamper or Mentor based on role
            if (account.Role.ToLower() == "bootcamper")
            {
                // Create Bootcamper and associate with Account
                var bootcamper = new Bootcamper
                {
                    Name = account.Name,
                };

                createdAccount.Bootcamper = bootcamper;
                createdAccount.BootcamperId = bootcamper.BootcamperId;

                // Add the Bootcamper to the context and save it
                await _bootcamperRepository.CreateBootcamper(bootcamper);
            }
            else if (account.Role.ToLower() == "mentor")
            {
                // Create Mentor and associate with Account
                var mentor = new Mentor
                {
                    MentorName = account.Name
                };

                createdAccount.Mentor = mentor;
                createdAccount.MentorId = mentor.MentorId;

                // Add the Mentor to the context and save it
                await _mentorRepository.CreateMentor(mentor);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(AccountDTO accountDto)
        {
            var account = await _authRepository.Login(accountDto.UsernameEmail, accountDto.Password);

            if (account == null)
                return Unauthorized("Invalid username or password");

            // Create JWT claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
        new Claim(ClaimTypes.Name, account.Username),
        new Claim(ClaimTypes.Role, account.Role),
        //new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(1).ToString())
    };

            // Generate JWT Token
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AppSettings:Token"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Store the JWT token in HttpOnly cookie
            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,  // Security measure: prevents JavaScript access
                Secure = true,  // Only send over HTTPS
                Expires = DateTime.UtcNow.AddHours(1),
                SameSite = SameSiteMode.Strict
            });

            // Store the user role in a separate cookie (optional)
            Response.Cookies.Append("role", account.Role, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            // Store the bootcamperId or mentorId in a cookie (optional)
            if (account.BootcamperId.HasValue)
            {
                Response.Cookies.Append("bootcamperId", account.BootcamperId.Value.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                Response.Cookies.Append("bootcamperName", account.Bootcamper.Name, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }
            else if (account.MentorId.HasValue)
            {
                Response.Cookies.Append("mentorId", account.MentorId.Value.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                Response.Cookies.Append("mentorName", account.Mentor.MentorName, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
            }

            // Return a minimal response (no token in body, since it's in the cookie)
            return Ok(new
            {
                message = "Login successful",
                role = account.Role,
                mentorId = account.MentorId,  // Only if applicable
                bootcamperId = account.BootcamperId, // Only if applicable
                mentorName = account.Mentor?.MentorName, // Add mentor name if applicable
                bootcamperName = account.Bootcamper?.Name, // Add bootcamper name if applicable
                jwt = tokenString // this might not be secure
            });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authRepository.Logout();
            return Ok(new { message = "Logged out successfully" });
        }


    }


}
