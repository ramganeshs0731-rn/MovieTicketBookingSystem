using MovieTicketBookingSystem.DTOs.Shows;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository _showRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IScreenRepository _screenRepository;

    public ShowService(IShowRepository showRepository, IMovieRepository movieRepository, IScreenRepository screenRepository)
    {
        _showRepository = showRepository;
        _movieRepository = movieRepository;
        _screenRepository = screenRepository;
    }

    public async Task<IEnumerable<ShowResponseDto>> GetAllShowsAsync()
    {
        var shows = await _showRepository.GetAllAsync();
        return shows.Select(s => MapToDto(s));
    }

    public async Task<ShowResponseDto?> GetShowByIdAsync(int id)
    {
        var s = await _showRepository.GetByIdAsync(id);
        return s == null ? null : MapToDto(s);
    }

    public async Task<IEnumerable<ShowResponseDto>> GetShowsByMovieIdAsync(int movieId)
    {
        var shows = await _showRepository.GetByMovieIdAsync(movieId);
        return shows.Select(s => MapToDto(s));
    }

    public async Task<IEnumerable<ShowResponseDto>> GetShowsByScreenIdAsync(int screenId)
    {
        var shows = await _showRepository.GetByScreenIdAsync(screenId);
        return shows.Select(s => MapToDto(s));
    }

    public async Task<ShowResponseDto> CreateShowAsync(CreateShowDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(dto.MovieId);
        if (movie == null) throw new Exception("Movie not found.");

        var screen = await _screenRepository.GetByIdAsync(dto.ScreenId);
        if (screen == null) throw new Exception("Screen not found.");

        var show = new Show
        {
            MovieId = dto.MovieId,
            ScreenId = dto.ScreenId,
            ShowTime = dto.ShowTime,
            TicketPrice = dto.TicketPrice
        };

        await _showRepository.AddAsync(show);

        // Fetch back with relations for response
        var createdShow = await _showRepository.GetByIdAsync(show.ShowId);
        return MapToDto(createdShow!);
    }

    public async Task UpdateShowAsync(int id, CreateShowDto dto)
    {
        var show = await _showRepository.GetByIdAsync(id);
        if (show != null)
        {
            show.MovieId = dto.MovieId;
            show.ScreenId = dto.ScreenId;
            show.ShowTime = dto.ShowTime;
            show.TicketPrice = dto.TicketPrice;
            await _showRepository.UpdateAsync(show);
        }
    }

    public async Task DeleteShowAsync(int id)
    {
        await _showRepository.DeleteAsync(id);
    }

    private ShowResponseDto MapToDto(Show s) => new()
    {
        ShowId = s.ShowId,
        MovieId = s.MovieId,
        MovieTitle = s.Movie?.Title ?? string.Empty,
        ScreenId = s.ScreenId,
        ScreenName = s.Screen?.Name ?? string.Empty,
        TheatreName = s.Screen?.Theatre?.Name ?? string.Empty,
        ShowTime = s.ShowTime,
        TicketPrice = s.TicketPrice
    };
}