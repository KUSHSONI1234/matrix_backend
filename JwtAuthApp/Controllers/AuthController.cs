using JwtAuthApp.Models;
using JwtAuthApp.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JwtAuthApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Register User
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_context.Users.Any(u => u.Username == model.Username))
                return BadRequest(new { message = "Username already exists." });

            var user = new User
            {
                Username = model.Username,
                PasswordHash = model.Password // Password is already hashed from frontend
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully." });
        }

        // User Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null || user.PasswordHash != model.Password)
                return Unauthorized(new { message = "Invalid username or password." });

            var token = GenerateJwtToken(model.Username);

            ///////user
            return Ok(new
            {
                token,
                // pagelist
                message = "Login successful.",
                expiresIn = 60 * 1000 // 60 seconds
            });
        }

        // Get All Users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Select(u => new
            {
                id = u.Id,
                username = u.Username,
                password = u.PasswordHash
            }).ToList();

            return Ok(users);
        }

        // Save Page Rights
     
        // Generate JWT Token for Authentication
        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
