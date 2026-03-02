using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MinimalApi.Models.DTOs;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // For now, we use a simple hardcoded check. 
            // In a real app, you would check this against your database.
            if (loginDto.Username == "admin" && loginDto.Password == "password123")
            {
                var token = GenerateJWTToken(loginDto.Username);
                return Ok(new { token = token });
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

        private string GenerateJWTToken(string username)
        {
            // 1. Get the secret key from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            /*
            This is how handshake Works:
               1. The Key: It pulls the same secret key from appsettings.json that the middleware uses to validate.
               2. The Signature: It signs the token using HmacSha256. If even one character in the token is changed, the signature breaks.
               3. The Result: It returns a long, encrypted string to Postman.
            */

            // 2. Create claims (User info inside the token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 3. Create the Token object
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            // 4. Return the generated string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}