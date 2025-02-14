using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.DTO;
using QuizAPI.Contract.Interface;
using QuizAPI.Contract.Repository;
using QuizAPI.Data;
using QuizAPI.Domain.DTO;
using QuizAPI.Model;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IMapper _mapper;

        public MentorController(IMentorRepository mentorRepository, IMapper mapper)
        {
            _mentorRepository = mentorRepository;
            _mapper = mapper;
        }

        // GET: api/Mentor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListMentorDTO>>> GetMentors()
        {
            var mentors = await _mentorRepository.Get();
            if (mentors == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ListMentorDTO>>(mentors));
        }

        // GET: api/Mentor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListMentorDTO>> GetMentor(int id)
        {
            var mentor = await _mentorRepository.GetById(id);

            if (mentor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ListMentorDTO>(mentor));
        }

        // PUT: api/Mentor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMentor(int id, Mentor mentor)
        {
            throw new NotImplementedException();
        }

        // POST: api/Mentor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MentorDTO>> PostMentor(MentorDTO mentor)
        {
            var mappedMentor = _mapper.Map<Mentor>(mentor);
            var createdMt = await _mentorRepository.CreateMentor(mappedMentor);
            if (createdMt == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetMentor), new { id = createdMt.MentorId }, _mapper.Map<ListMentorDTO>(mentor));
            //return StatusCode(201);
        }

        // DELETE: api/Mentor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            var mentor = await _mentorRepository.DeleteMentor(id);
            return NoContent();
        }
    }
}
