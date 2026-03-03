
using MinimalApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Services;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Register : ControllerBase
    {
        private readonly IUserService _userService;
        public Register(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registeruser")]
        public IActionResult RegisterUser([FromBody] RegisterDto user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            var existingUser = _userService.GetUserByUsername(user.Username);

            if (existingUser != null)
            {
                return Conflict("Username already exists. Please choose a different username.");
            }

            var newUser = new LoginDto
            {
                Username = user.Username,
                Password = user.Password
            };

            _userService.AddUser(newUser);
            return Ok("User registered successfully.");
        }
    }
}