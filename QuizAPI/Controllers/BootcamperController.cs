using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Contract.Interface;
using QuizAPI.Domain.DTO;
using QuizAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BootcamperController : ControllerBase
    {
        private readonly IBootcamperRepository _bootcamperRepository;
        private readonly IMapper _mapper;

        public BootcamperController(IBootcamperRepository bootcamperRepository, IMapper mapper)
        {
            _bootcamperRepository = bootcamperRepository;
            _mapper = mapper;
        }
        // GET: api/<BootcamperController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BootcamperDTO>>> GetAsync()
        {
            var bootcampers = await _bootcamperRepository.Get();
            if(bootcampers == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<BootcamperDTO>>(bootcampers));
        }

        // GET api/<BootcamperController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListOneBootcamperDTO>> GetById([FromRoute] int id)
        {
            var bootcamper = await _bootcamperRepository.GetById(id);
            if (bootcamper == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ListOneBootcamperDTO>(bootcamper));
        }

        // POST api/<BootcamperController>
        [HttpPost]
        public async Task<ActionResult<BootcamperDTO>> PostAsync([FromBody] BootcamperDTO bootcamper)
        {
            var mappedBootcamper = _mapper.Map<Bootcamper>(bootcamper);
            Console.WriteLine(mappedBootcamper.Name);
            var createdBc = await _bootcamperRepository.CreateBootcamper(mappedBootcamper);
            if(createdBc == null)
            {
                return BadRequest();
            }
            Console.WriteLine(createdBc.BootcamperId);
            return CreatedAtAction(nameof(GetById), new {id = createdBc.BootcamperId}, _mapper.Map<BootcamperDTO>(bootcamper));
            //return StatusCode(201);
        }

        // PUT api/<BootcamperController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BootcamperController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var bootcamper = await _bootcamperRepository.DeleteBootcamper(id);
            return NoContent();
        }
    }
}
