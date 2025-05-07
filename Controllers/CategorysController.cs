using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheraGuide.Entity;
using TheraGuide.Repository;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryReopsitory;

        public CategorysController(
            IGenericRepository<Category> categoryReopsitory)
        {
            _categoryReopsitory = categoryReopsitory;
        }
        // GET: api/category/my
        [HttpGet("list")]
        public async Task<IActionResult> GetMycategory()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _categoryReopsitory.GetAllAsync();
            return Ok(category);
        }

      
    }

}

