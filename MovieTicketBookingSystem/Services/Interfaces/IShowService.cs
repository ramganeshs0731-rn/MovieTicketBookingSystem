using MovieTicketBookingSystem.DTOs.Shows;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface IShowService
{
    Task<IEnumerable<ShowResponseDto>> GetAllShowsAsync();
    Task<ShowResponseDto?> GetShowByIdAsync(int id);
    Task<IEnumerable<ShowResponseDto>> GetShowsByMovieIdAsync(int movieId);
    Task<IEnumerable<ShowResponseDto>> GetShowsByScreenIdAsync(int screenId);
    Task<ShowResponseDto> CreateShowAsync(CreateShowDto dto);
    Task UpdateShowAsync(int id, CreateShowDto dto);
    Task DeleteShowAsync(int id);
}