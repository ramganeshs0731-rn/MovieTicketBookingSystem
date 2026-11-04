using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Seats;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatsController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var seats = await _seatService.GetAllSeatsAsync();
        return Ok(seats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var seat = await _seatService.GetSeatByIdAsync(id);
        if (seat == null) return NotFound();
        return Ok(seat);
    }

    [HttpGet("screen/{screenId}")]
    public async Task<IActionResult> GetByScreen(int screenId)
    {
        var seats = await _seatService.GetSeatsByScreenIdAsync(screenId);
        return Ok(seats);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeatDto dto)
    {
        try
        {
            var created = await _seatService.CreateSeatAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SeatId }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSeatDto dto)
    {
        await _seatService.UpdateSeatAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _seatService.DeleteSeatAsync(id);
        return NoContent();
    }
}