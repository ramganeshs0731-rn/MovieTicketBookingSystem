using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Bookings;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Requires a valid JWT token for all endpoints in this controller
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _bookingService.GetBookingByIdAsync(id);
        if (booking == null) return NotFound();
        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        try
        {
            var created = await _bookingService.CreateBookingAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BookingId }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}