using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookOrGetBooked.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingStatusController : ControllerBase
{
    private readonly IBookingStatusService _bookingStatusService;
    private readonly IMapper _mapper;

    public BookingStatusController(IBookingStatusService bookingStatusService, IMapper mapper)
    {
        _bookingStatusService = bookingStatusService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingStatusSummaryDTO>>> GetAll()
    {
        var result = await _bookingStatusService.GetAllAsync();

        if (!result.IsSuccess || result.Data is null)
            return StatusCode(500, "Failed to retrieve statuses.");

        return Ok(result.Data);
    }
}
