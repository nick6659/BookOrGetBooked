using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookOrGetBooked.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDTO bookingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Ensure validation for request data
            }

            var result = await _bookingService.CreateBookingAsync(bookingRequest);
            return Ok(result);  // Returns BookingResponseDTO
        }

        // GET: api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);  // Can return BookingResponseDTO
        }
    }
}
