using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BallastLaneApplication.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BallastLaneApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        //GET: api/Users/asdasdasd
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserId(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //GET: api/Users/teste
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserUserName(string userName)
        {
            var user = await _userService.GetByUserNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Users/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(user);

            createdUser.PasswordHash = "";

            return CreatedAtAction(nameof(GetUserId),new { id = createdUser.Id}, createdUser);
        }

        // POST: api/Users/authenticate
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDto userLogin)
        {
            var user = await _userService.AuthenticateAsync(userLogin.Username, userLogin.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken(UserEntitie user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim("Task", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
