using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Screens;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScreensController : ControllerBase
{
    private readonly IScreenService _screenService;

    public ScreensController(IScreenService screenService)
    {
        _screenService = screenService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var screens = await _screenService.GetAllScreensAsync();
        return Ok(screens);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var screen = await _screenService.GetScreenByIdAsync(id);
        if (screen == null) return NotFound();
        return Ok(screen);
    }

    [HttpGet("theatre/{theatreId}")]
    public async Task<IActionResult> GetByTheatre(int theatreId)
    {
        var screens = await _screenService.GetScreensByTheatreIdAsync(theatreId);
        return Ok(screens);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScreenDto dto)
    {
        try
        {
            var created = await _screenService.CreateScreenAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ScreenId }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateScreenDto dto)
    {
        await _screenService.UpdateScreenAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _screenService.DeleteScreenAsync(id);
        return NoContent();
    }
}