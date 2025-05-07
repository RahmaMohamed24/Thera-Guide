using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheraGuide.Context;
using TheraGuide.Entity;
using TheraGuide.Repository;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly ApplicationDbContext _context;
        public QuestionsController(IGenericRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpGet]
        public async Task<ActionResult<object>> GetAllQuestionsWithAnswers(long? CategoryId)
        {
            var questions = await _context.Questions
                .Include(q => q.Answers)
                .Where(q=>(CategoryId ==null || q.CategoryId == CategoryId))
                .ToListAsync();

            var response = new
            {
                questions = questions.Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    Content = q.Content,
                    Answers = q.Answers.Select(a => new Answer { Id = a.Id, Content = a.Content }).ToList(),
                    CategoryId = q.CategoryId
                }).ToList()
            };

            return Ok(response);
        }

        //// GET: api/questions
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions()
        //{
        //    var questions = await _questionRepository.GetAllAsync();
        //    return Ok(questions);
        //}

        // GET: api/questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(long id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // POST: api/questions
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _questionRepository.InsertAsync(question);
            return CreatedAtAction(nameof(GetQuestion), new { id = created.Id }, created);
        }

        // PUT: api/questions/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestion(long id, [FromBody] Question question)
        //{
        //    if (id != question.Id)
        //        return BadRequest("ID mismatch");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var existing = await _questionRepository.GetByIdAsync(id);
        //    if (existing == null)
        //        return NotFound();

        //    await _questionRepository.UpdateAsync(question);
        //    return NoContent();
        //}

        //// DELETE: api/questions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuestion(long id)
        //{
        //    var existing = await _questionRepository.GetByIdAsync(id);
        //    if (existing == null)
        //        return NotFound();

        //    await _questionRepository.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
