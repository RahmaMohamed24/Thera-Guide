using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheraGuide.EmailSender;
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
        private readonly IEmailSender _emailSender;
        public AccountController(
            IGenericRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            IEmailSender emailSender
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<ApplicationUser>(model);

            // Generate and set verification code
            var verificationCode = new Random().Next(100000, 999999).ToString();
            user.VerificationCode = verificationCode;
            user.IsEmailVerified = false;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                try
                {
                    var subject = "Email Verification - TheraGuide";
                    var message = $"Hi {user.UserName},\n\nYour Email Verification Code Is: {verificationCode}";

                    await _emailSender.SendEmailAsync(user.Email, subject, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send verification email: {ex.Message}");
                }

                return Ok(new { Message = "User registered successfully. Please verify your email.", model });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound("User not found.");

            if (user.VerificationCode != model.VerificationCode)
                return BadRequest("Invalid verification code.");

            user.IsEmailVerified = true;
            user.VerificationCode = "";

            await _userManager.UpdateAsync(user);

            return Ok(new { Message = "Email verified successfully." });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordValid)
                return Unauthorized("Invalid credentials");

            //  Check if email is verified
            if (!user.IsEmailVerified)
                return Unauthorized("Please verify your email before logging in.");

            //  Generate JWT token
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                UserId = user.Id,
                Email = user.Email
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
          
            //await _signInManager.SignOutAsync();

            

            return Ok(new { Message = "Logged out successfully" });
        }

    }
}
