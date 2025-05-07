using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheraGuide.Context;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetTipsByCategory/{categoryId}")]
        public ActionResult<List<TipViewModel>> GetTipsByCategory(long? categoryId)
        {
            var tips = _context.Tips
                .Where(t =>(categoryId ==null || t.CategoryId == categoryId))
                .Select(t => new TipViewModel
                {
                    Type = t.Type,
                    Content = t.Content,
                    CategoryName = t.Category.Name
                })
                .ToList();

            if (!tips.Any())
            {
                return NotFound($"No tips found for category ID {categoryId}.");
            }

            return Ok(tips);
        }
    }
}
