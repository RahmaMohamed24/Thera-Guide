using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheraGuide.Entity;
using TheraGuide.Repository;
using TheraGuide.ViewModels;

namespace TheraGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(
            IGenericRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var User = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(User, model.Password);

            if (result.Succeeded)
            {

                return Ok(new { Message = "User registered successfully",model });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    // Add more claims if needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return token, userId, and any other relevant user info
            return Ok(new
            {
                Token = tokenString,
                UserId = user.Id,
                Email = user.Email,
                // Add any other user properties you need
            });
        }

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate userId is provided
            if (string.IsNullOrEmpty(model.UserId))
            {
                return BadRequest("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update the fields
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Profile updated successfully" });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
          
            await _signInManager.SignOutAsync();

            

            return Ok(new { Message = "Logged out successfully" });
        }

    }
}
