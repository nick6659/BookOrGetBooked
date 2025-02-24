using BookOrGetBooked.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookOrGetBooked.Shared.Filters;

namespace BookOrGetBooked.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
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


    }
}
