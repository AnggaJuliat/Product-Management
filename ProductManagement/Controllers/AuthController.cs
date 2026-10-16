using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Common;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("/")]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("api/auth/register")]
        public IActionResult ApiRegister([FromBody] RegisterDto? dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { Message = "Username dan password wajib diisi." });
            }

            if (DataStore.Users.Any(u => u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new { Message = "Username sudah digunakan." });
            }

            DataStore.Users.Add(new User { Username = dto.Username, Password = dto.Password });
            _logger.LogInformation("Pengguna baru terdaftar: {Username}", dto.Username);
            return Ok(new { Message = "Registrasi berhasil, silakan login." });
        }

        [HttpPost("api/auth/login")]
        public IActionResult ApiLogin([FromBody] LoginDto? dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { Message = "Username dan password wajib diisi." });
            }

            var user = DataStore.Users.FirstOrDefault(u => 
                u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase) && 
                u.Password == dto.Password);

            if (user == null)
            {
                _logger.LogWarning("Percobaan login gagal untuk: {Username}", dto.Username);
                return Unauthorized(new { Message = "Username atau password salah." });
            }

            var token = GenerateJwtToken(user.Username);
            _logger.LogInformation("Pengguna berhasil login: {Username}", dto.Username);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}