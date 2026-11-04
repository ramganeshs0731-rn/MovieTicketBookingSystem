using MovieTicketBookingSystem.DTOs.Movies;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return movies.Select(MapToResponse);
    }

    public async Task<MovieResponse?> GetMovieByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie == null ? null : MapToResponse(movie);
    }

    public async Task<IEnumerable<MovieResponse>> SearchMoviesByTitleAsync(string title)
    {
        var movies = await _movieRepository.SearchByTitleAsync(title);
        return movies.Select(MapToResponse);
    }

    public async Task<MovieResponse> CreateMovieAsync(CreateMovieRequest request)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Description = request.Description,
            DurationMinutes = request.DurationMinutes,
            ReleaseDate = request.ReleaseDate,
            Language = request.Language,
            Genre = request.Genre
        };

        var createdMovie = await _movieRepository.AddAsync(movie);
        return MapToResponse(createdMovie);
    }

    public async Task<bool> UpdateMovieAsync(int id, UpdateMovieRequest request)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null) return false;

        movie.Title = request.Title;
        movie.Description = request.Description;
        movie.DurationMinutes = request.DurationMinutes;
        DateOnly releaseDate = request.ReleaseDate;
        movie.ReleaseDate = releaseDate;
        movie.Language = request.Language;
        movie.Genre = request.Genre;

        await _movieRepository.UpdateAsync(movie);
        return true;
    }

    public async Task<bool> DeleteMovieAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null) return false;

        if (await _movieRepository.HasShowsAsync(id))
        {
            throw new InvalidOperationException("Cannot delete movie because scheduled shows depend on it.");
        }

        await _movieRepository.DeleteAsync(movie);
        return true;
    }

    private static MovieResponse MapToResponse(Movie movie)
    {
        return new MovieResponse
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Language = movie.Language,
            Genre = movie.Genre
        };
    }
}