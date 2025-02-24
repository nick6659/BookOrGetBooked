using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookOrGetBooked.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO userCreateDto)
        {
            var result = await _userService.CreateAsync(userCreateDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        /*
        // GET: api/user/exists/{id}
        [HttpGet("exists/{id}")]
        public async Task<IActionResult> UserExists(int id)
        {
            var result = await _userService.ExistsAsync(id);

            if (!result.IsSuccess || !result.Data)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { exists = result.Data });
        }
        */

        /*
        // GET: api/user/email/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            // Assuming GetUserByEmailAsync is implemented in IUserService
            var result = await _userService.GetUserByEmailAsync(email);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
        */
    }
}
