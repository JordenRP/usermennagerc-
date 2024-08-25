using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementApp.Models;
using UserManagementApp.Services;
using UserManagementApp.DTOs;
using UserManagementApp.Helpers;

namespace UserManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IUserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(registrationDto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registrationDto.Username,
                PasswordHash = registrationDto.Password,
                Email = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName
            };

            await _userService.CreateUserAsync(user);

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _userService.GetUserByUsernameAsync(loginDto.Username);

            if (user == null || !_userService.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtHelper.GenerateJwtToken(user.Id);

            return Ok(new { Token = token , UserId = user.Id.ToString()});
        }
    }
}
