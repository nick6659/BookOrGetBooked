using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using Microsoft.AspNetCore.Mvc;

namespace BookOrGetBooked.API.Controllers;

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
        var statuses = await _bookingStatusService.GetAllAsync();
        var result = _mapper.Map<List<BookingStatusSummaryDTO>>(statuses);
        return Ok(result);
    }
}
