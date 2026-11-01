using Microsoft.AspNetCore.Mvc;
using MovieTicketBookingSystem.DTOs.Movies;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovieResponse>> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
            return NotFound(new { message = $"Movie with ID {id} not found." });

        return Ok(movie);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<MovieResponse>>> SearchMovies([FromQuery] string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return BadRequest(new { message = "Search title parameter is required." });

        var movies = await _movieService.SearchMoviesByTitleAsync(title);
        return Ok(movies);
    }

    [HttpPost]
    public async Task<ActionResult<MovieResponse>> CreateMovie([FromBody] CreateMovieRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (request.DurationMinutes <= 0)
            return BadRequest(new { message = "Duration must be greater than zero." });

        var createdMovie = await _movieService.CreateMovieAsync(request);
        return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.MovieId }, createdMovie);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateMovie(int id, [FromBody] UpdateMovieRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (request.DurationMinutes <= 0)
            return BadRequest(new { message = "Duration must be greater than zero." });

        var success = await _movieService.UpdateMovieAsync(id, request);
        if (!success)
            return NotFound(new { message = $"Movie with ID {id} not found." });

        return Ok(new { message = "Movie updated successfully." });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        try
        {
            var success = await _movieService.DeleteMovieAsync(id);
            if (!success)
                return NotFound(new { message = $"Movie with ID {id} not found." });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}