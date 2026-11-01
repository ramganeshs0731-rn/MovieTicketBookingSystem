using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Theatres;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheatresController : ControllerBase
{
    private readonly ITheatreService _theatreService;

    public TheatresController(ITheatreService theatreService)
    {
        _theatreService = theatreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var theatres = await _theatreService.GetAllTheatresAsync();
        return Ok(theatres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var theatre = await _theatreService.GetTheatreByIdAsync(id);
        if (theatre == null) return NotFound();
        return Ok(theatre);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTheatreDto dto)
    {
        var created = await _theatreService.CreateTheatreAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.TheatreId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTheatreDto dto)
    {
        await _theatreService.UpdateTheatreAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _theatreService.DeleteTheatreAsync(id);
        return NoContent();
    }
}