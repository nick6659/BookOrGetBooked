using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookOrGetBooked.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        // GET: api/usertype
        [HttpGet]
        public async Task<IActionResult> GetAllUserTypes()
        {
            var result = await _userTypeService.GetAllAsync();

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        // GET: api/usertype/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTypeById(int id)
        {
            var result = await _userTypeService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }
    }
}
