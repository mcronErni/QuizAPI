using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.DTO;
using QuizAPI.Contract.Interface;
using QuizAPI.Model;

namespace QuizAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BootcamperQuizController : ControllerBase
    {
        private readonly IBootcamperQuizRepository _bootcamperQuizRepository;
        private readonly IMapper _mapper;

        public BootcamperQuizController(IBootcamperQuizRepository bootcamperQuizRepository, IMapper mapper)
        {
            _bootcamperQuizRepository = bootcamperQuizRepository;
            _mapper = mapper;

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "mentor")]
        public async Task<ActionResult<IEnumerable<BootcamperQuizDTO>>> GetAll([FromRoute] int id)
        {
            var bq = await _bootcamperQuizRepository.GetAllBootcamperQuizByQuizId(id);
            if(bq == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<ICollection<BootcamperQuizDTO>>(bq);
            return Ok(mapped);
        }

        [HttpGet("bootcamper/{id}")]
        [Authorize(Roles = "bootcamper")]
        public async Task<ActionResult<IEnumerable<BootcamperQuizDTO>>> GetAllForBootcamper([FromRoute] int id)
        {
            var bq = await _bootcamperQuizRepository.GetBootcamperQuizForBootcamperAllQuiz(id);
            if (bq == null)
            {
                return NotFound();
            }
            var mapped = _mapper.Map<ICollection<BootcamperQuizDTO>>(bq);
            return Ok(mapped);
        }

        [Authorize(Roles = "mentor")]
        [HttpGet("{bootcamperId}/{quizId}")]
        public async Task<ActionResult<BootcamperQuizDTO>> GetById([FromRoute] int bootcamperId, [FromRoute]int quizId)
        {

            var bq = await _bootcamperQuizRepository.GetBootcamperQuizForBootcamper(bootcamperId, quizId);
            if (bq == null)
            {
                return NotFound(new { Message="No Entry with that ID"});
            }
            var mapped = _mapper.Map<BootcamperQuizDTO>(bq);
            return Ok(mapped);
        }

        [Authorize(Roles = "bootcamper")]
        [HttpPost]
        public async Task<ActionResult<BootcamperQuizDTO>> AddBootcamperQuiz(AddBootcamperQuizDTO addBootcamperQuizDTO)
        {
            try
            {
                var mappedBq = _mapper.Map<BootcamperQuiz>(addBootcamperQuizDTO);
                var createdBq = await _bootcamperQuizRepository.CreateBootcamperQuiz(mappedBq);
                if (createdBq == null)
                {
                    return BadRequest(new { Message = "No Entry with that ID" });
                }
                var mapped = _mapper.Map<BootcamperQuizDTO>(createdBq);
                return Ok(mapped);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627) // Unique constraint violation
            {
                return Conflict("Quiz submission already exists.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while submitting the quiz.");
            }
        }
    }
}
