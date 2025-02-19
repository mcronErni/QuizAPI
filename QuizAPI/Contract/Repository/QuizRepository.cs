using Microsoft.EntityFrameworkCore;
using QuizAPI.Contract.Interface;
using QuizAPI.Data;
using QuizAPI.Model;

namespace QuizAPI.Contract.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Quiz?> CreateQuiz(Quiz quiz)
        {
            quiz.IsDeleted = false;
            var created_quiz = await _context.Quizzes.AddAsync(quiz);
            if (created_quiz == null) { return null; }
            await _context.SaveChangesAsync();
            return quiz;

        }

        public async Task<Quiz?> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.QuizId == id);
            if (quiz == null) { return null; }
            quiz.IsDeleted = true;
            //_context.Remove(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<ICollection<Quiz>> Get()
        {
            var quizzes = await _context.Quizzes
                .Include(m => m.Mentor)
                .Where(q => q.IsDeleted == false)
                .ToListAsync();
            return quizzes;
        }

        public async Task<Quiz>? GetById(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .Where(q => q.IsDeleted == false)
                .FirstOrDefaultAsync(q => q.QuizId.Equals(id));
            if(quiz == null)
            {
                return null;
            }
            return quiz;
        }

        public async Task<ICollection<Quiz>?> GetByMentorId(int mentorId)
        {
            var quizzes = await _context.Quizzes
                .Include (q => q.Mentor)
                .Where(q => q.MentorId == mentorId && q.IsDeleted == false)
                .ToListAsync();

            return quizzes;
        }

        public async Task<Quiz>? UpdateQuiz(int id, Quiz quiz)
        {
            Console.WriteLine(quiz.QuizId);
            Console.WriteLine(id);
            // Load the existing quiz including its related questions
            var quizToBeEdited = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quizToBeEdited == null)
            {
                return null;
            }

            // Update the properties of the quiz
            quizToBeEdited.QuizTitle = quiz.QuizTitle;
            quizToBeEdited.TotalScore = quiz.TotalScore;

            // Update the related questions
            // First, remove any questions that are no longer in the updated quiz
            var questionsToRemove = quizToBeEdited.Questions
                .Where(q => !quiz.Questions.Any(q2 => q2.QuestionId == q.QuestionId))
                .ToList();
            foreach (var question in questionsToRemove)
            {
                _context.Questions.Remove(question);
            }

            // Next, update existing questions and add new ones
            foreach (var question in quiz.Questions)
            {
                var existingQuestion = quizToBeEdited.Questions
                    .FirstOrDefault(q => q.QuestionId == question.QuestionId);
                if (existingQuestion != null)
                {
                    // Update existing question
                    existingQuestion.MQuestion = question.MQuestion;
                    existingQuestion.Answer = question.Answer;
                    // Update other properties as needed
                }
                else
                {
                    // Add new question
                    quizToBeEdited.Questions.Add(question);
                }
            }

            await _context.SaveChangesAsync();
            return quizToBeEdited;
        }

    }
}
