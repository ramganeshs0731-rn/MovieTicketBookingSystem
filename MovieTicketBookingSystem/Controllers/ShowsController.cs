using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Shows;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowsController : ControllerBase
{
    private readonly IShowService _showService;

    public ShowsController(IShowService showService)
    {
        _showService = showService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shows = await _showService.GetAllShowsAsync();
        return Ok(shows);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var show = await _showService.GetShowByIdAsync(id);
        if (show == null) return NotFound();
        return Ok(show);
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetByMovie(int movieId)
    {
        var shows = await _showService.GetShowsByMovieIdAsync(movieId);
        return Ok(shows);
    }

    [HttpGet("screen/{screenId}")]
    public async Task<IActionResult> GetByScreen(int screenId)
    {
        var shows = await _showService.GetShowsByScreenIdAsync(screenId);
        return Ok(shows);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShowDto dto)
    {
        try
        {
            var created = await _showService.CreateShowAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ShowId }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateShowDto dto)
    {
        await _showService.UpdateShowAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _showService.DeleteShowAsync(id);
        return NoContent();
    }
}