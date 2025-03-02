﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Contract.Interface;
using QuizAPI.Domain.DTO;
using QuizAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuizAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizController(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        // GET: api/<BootcamperController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListQuizDTO>>> GetAsync()
        {
            var quizzes = await _quizRepository.Get();
            if(quizzes == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ListQuizDTO>>(quizzes));
        }

        // GET api/<BootcamperController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetById([FromRoute] int id)
        {
            var quiz = await _quizRepository.GetById(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<QuizDTO>(quiz));
        }

        // POST api/<BootcamperController>
        [Authorize(Roles = "mentor")]
        [HttpPost]
        public async Task<ActionResult<QuizDTO>> PostAsync([FromBody] QuizDTO quiz)
        {

            foreach (var question in quiz.Questions) 
            {
                if (!question.Choices.Contains(question.Answer)){
                    question.Choices.Add(question.Answer);
                }
            }

            var mappedQuiz = _mapper.Map<Quiz>(quiz);
            var createdQuiz = await _quizRepository.CreateQuiz(mappedQuiz);
            if(createdQuiz == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new {id = createdQuiz.QuizId}, _mapper.Map<QuizDTO>(createdQuiz));
            //return StatusCode(201);
        }

        // PUT api/<BootcamperController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<QuizDTO>> Put([FromRoute]int id, [FromBody] QuizDTO quiz)
        {
            var mappedQuiz = _mapper.Map<Quiz>(quiz);
            var updatedQuiz = await _quizRepository.UpdateQuiz(id,mappedQuiz);
            if (updatedQuiz == null)
            {
                return NotFound(new { Message="No Quiz to Edit"});
            }
            //return CreatedAtAction(nameof(GetById), new { id = updatedQuiz.Id }, _mapper.Map<QuizDTO>(updatedQuiz));
            return Ok();
        }

        // DELETE api/<BootcamperController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var quiz = await _quizRepository.DeleteQuiz(id);
            return NoContent();
        }

        [Authorize(Roles = "mentor")]
        [HttpGet("Mentor/{id}")]
        public async Task<ActionResult<ListQuizDTO>> GetByMentorId(int id)
        {
            var quizzes = await _quizRepository.GetByMentorId(id);

            if(quizzes == null)
            {
                return NotFound(new {Message="No quizzes"});
            }
            return Ok(_mapper.Map<IEnumerable<ListQuizDTO>>(quizzes));
        }
    }
}
