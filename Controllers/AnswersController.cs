using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheraGuide.Entity;
using TheraGuide.Repository;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IGenericRepository<Answer> _answerRepository;
        private readonly IGenericRepository<Question> _questionRepository;

        public AnswersController(IGenericRepository<Answer> answerRepository, IGenericRepository<Question> questionRepository)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        // GET: api/answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAllAnswers()
        {
            var answers = await _answerRepository.GetAllAsync();
            return Ok(answers);
        }

        // GET: api/answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(long id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
                return NotFound();

            return Ok(answer);
        }

        // POST: api/answers
        [HttpPost]
        public async Task<ActionResult<Answer>> CreateAnswer([FromBody] Answer answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionExists = await _questionRepository.GetByIdAsync(answer.QuestionId);
            if (questionExists == null)
                return BadRequest($"No Question found with ID {answer.QuestionId}");

            var created = await _answerRepository.InsertAsync(answer);
            return CreatedAtAction(nameof(GetAnswer), new { id = created.Id }, created);
        }

        //// PUT: api/answers/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAnswer(long id, [FromBody] Answer answer)
        //{
        //    if (id != answer.Id)
        //        return BadRequest("ID mismatch");

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var existing = await _answerRepository.GetByIdAsync(id);
        //    if (existing == null)
        //        return NotFound();

        //    await _answerRepository.UpdateAsync(answer);
        //    return NoContent();
        //}

        //// DELETE: api/answers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAnswer(long id)
        //{
        //    var existing = await _answerRepository.GetByIdAsync(id);
        //    if (existing == null)
        //        return NotFound();

        //    await _answerRepository.DeleteAsync(id);
        //    return NoContent();
        //}
    }

}
