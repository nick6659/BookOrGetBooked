using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookOrGetBooked.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        // Get all service types
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _serviceTypeService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }
            return Ok(result.Data);
        }

        // Get a specific service type by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceTypeService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }
            return Ok(result.Data);
        }

        // Create a new service type
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceTypeCreateDTO createDto)
        {
            var result = await _serviceTypeService.CreateAsync(createDto);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error });
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        // Update an existing service type
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceTypeUpdateDTO updateDto)
        {
            var result = await _serviceTypeService.UpdateAsync(id, updateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error });
            }
            return Ok(result.Data);
        }

        // Delete a service type
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceTypeService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }
            return NoContent();
        }
    }
}
