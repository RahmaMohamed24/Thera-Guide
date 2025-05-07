using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TheraGuide.Context;
using TheraGuide.Entity;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserAnswer([FromBody] SaveUserAnswerViewModel dto)
        {
            var userAnswer = new UserAnswers
            {
                UserId = dto.UserId,
                AnswerId = dto.AnswerId
            };

            _context.UserAnswers.Add(userAnswer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Answer saved successfully." });
        }
        [HttpPost]
        public async Task<IActionResult> SaveUserAnswer([FromBody] SaveUserAnswersViewModel dto)
        {

            var userAnswers = new List<UserAnswers>();
            foreach (long AnswerId in dto.AnswerIds)
            {
                var userAnswer = new UserAnswers
                {
                    UserId = dto.UserId,
                    AnswerId = AnswerId
                };
                userAnswers.Add(userAnswer);
            }
            await _context.UserAnswers.AddRangeAsync(userAnswers);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Answer saved successfully." });
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAnswersByUser(string userId)
        {
            var answerIds = await _context.UserAnswers
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AnswerId)
                .ToListAsync();

            return Ok(new { userId, answerIds });
        }

    }
}
