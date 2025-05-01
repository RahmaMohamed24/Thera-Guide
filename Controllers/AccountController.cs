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

                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser UserModel = await _userManager.FindByNameAsync(loginDto.Email);
                if (UserModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(UserModel, loginDto.Password);

                    if (found)

                    {
                        // Your code to generate a JWT token
                        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
        new Claim(ClaimTypes.NameIdentifier, UserModel.Id.ToString())
                            }),
                            Expires = DateTime.UtcNow.AddHours(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);

                        return Ok(new { Token = tokenString });
                       

                    }


                }
                return BadRequest();
            }


            //401
            return Unauthorized();
        }
       
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user's ID
            var user = await _userManager.FindByIdAsync(userId);

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

    }
}
