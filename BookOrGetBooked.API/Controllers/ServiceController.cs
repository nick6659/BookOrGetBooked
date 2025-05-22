using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs.Service;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookOrGetBooked.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceCreateDTO dto)
        {
            var result = await _serviceService.CreateServiceAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return Ok(result.Data);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetServices([FromBody] ServiceFilterParameters filters)
        {
            var result = await _serviceService.GetServicesAsync(filters);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var result = await _serviceService.GetAllServicesAsync(); // Call new service method

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        [HttpPost("filter/distance")]
        public async Task<IActionResult> GetServicesWithinDistance([FromBody] ServiceFilterParameters filters, [FromQuery] double userLat, [FromQuery] double userLon)
        {
            var result = await _serviceService.GetServicesWithinDistanceAsync(filters, userLat, userLon);

            if (!result.IsSuccess)
            {
                return NotFound(new { message = result.Error });
            }

            return Ok(result.Data);
        }


    }
}
