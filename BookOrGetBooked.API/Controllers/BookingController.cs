using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookOrGetBooked.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingController(
    IBookingService bookingService
    ) : ControllerBase
{
    // POST: api/booking
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO bookingRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await bookingService.CreateBookingAsync(bookingRequest);
        return Ok(result);
    }

    // GET: api/booking/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var result = await bookingService.GetBookingAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> GetBookings([FromBody] BookingFilterParameters filters)
    {
        var result = await bookingService.GetBookingsAsync(filters);

        if (!result.IsSuccess)
        {
            if (result.Error != null)
            {
                if (ErrorCodes.Validation.Messages.ContainsKey(result.Error.Code))
                {
                    return BadRequest(result); // Return 400 Bad Request with validation errors
                }
                if (result.Error.Code == ErrorCodes.Resource.NotFound)
                {
                    return NotFound(result); // Return 404 Not Found
                }
            }

            return StatusCode(500, result); // Return 500 Internal Server Error
        }

        return Ok(result.Data);
    }

    // PUT: api/booking/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDTO bookingUpdateRequest)
    {
        if (id <= 0)
        {
            return BadRequest(new { message = "Booking ID is invalid." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await bookingService.UpdateBookingAsync(id, bookingUpdateRequest);
        if (!result.IsSuccess)
        {
            if (result.Error != null)
            {
                if (result.Error.Code == ErrorCodes.Resource.NotFound)
                {
                    return NotFound(new { message = result.Error });
                }
            }

            return StatusCode(500, new { message = "An error occurred while updating the booking." });
        }

        return Ok(result.Data);
    }

    [HttpPatch("{id}/provider")]
    public async Task<IActionResult> UpdateBookingByProvider(int id, [FromBody] ServiceProviderBookingUpdateDTO dto)
    {
        var result = await bookingService.UpdateBookingByProviderAsync(id, dto);

        if (!result.IsSuccess)
        {
            if (result.Error?.Code == ErrorCodes.Resource.NotFound)
                return NotFound(new { message = result.Error });

            return StatusCode(500, result);
        }

        return Ok(result.Data);
    }

    /*
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeBookingStatus(int id, [FromBody] BookingStatusChangeDTO request)
    {
        if (request.BookingStatusId <= 0)
            return BadRequest(new { message = "Invalid status ID" });

        var result = await bookingService.ChangeBookingStatusAsync(id, request.BookingStatusId);

        if (!result.IsSuccess)
        {
            if (result.Error?.Code == ErrorCodes.Resource.NotFound)
                return NotFound(new { message = result.Error });

            return StatusCode(500, result);
        }

        return Ok(result.Data);
    }
    */

    /*
    // GET: api/booking
    public async Task<IActionResult> GetAllBookingsByUserId(int id)
    {
        var result = await _bookingService.GetAllBookingsByUserId(id);

        if (!result.IsSuccess)
        {
            return NotFound(new { message = result.Error });
        }

        return Ok(result.Data);
    }
    */

}
