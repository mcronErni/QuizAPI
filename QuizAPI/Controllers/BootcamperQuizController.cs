using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Contract.DTO;
using QuizAPI.Contract.Interface;
using QuizAPI.Model;

namespace QuizAPI.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BootcamperQuizDTO>>> GetAll()
        {
            var bq = await _bootcamperQuizRepository.GetAllBootcamperQuiz();
            return Ok(bq);
        }

        [HttpGet("{bootcamperId}/{quizId}")]
        public async Task<ActionResult<BootcamperQuizDTO>> GetById([FromRoute] int bootcamperId, int quizId)
        {

            var bq = await _bootcamperQuizRepository.GetBootcamperQuiz(bootcamperId, quizId);
            if (bq == null)
            {
                return NotFound(new { Message="No Entry with that ID"});
            }
            return Ok(bq);
        }

        [HttpPost]
        public async Task<ActionResult<BootcamperQuizDTO>> AddBootcamperQuiz(AddBootcamperQuizDTO addBootcamperQuizDTO)
        {
            var mappedBq = _mapper.Map<BootcamperQuiz>(addBootcamperQuizDTO);
            var createdBq = await _bootcamperQuizRepository.CreateBootcamperQuiz(mappedBq);
            if (createdBq == null)
            {
                return BadRequest(new { Message = "No Entry with that ID" });
            }
            return CreatedAtAction(nameof(GetById), new { bootcamperId = createdBq.BootcamperId, quizId = createdBq.QuizId }, createdBq);
        }
    }
}
