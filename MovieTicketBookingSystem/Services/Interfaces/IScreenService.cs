using MovieTicketBookingSystem.DTOs.Screens;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface IScreenService
{
    Task<IEnumerable<ScreenResponseDto>> GetAllScreensAsync();
    Task<ScreenResponseDto?> GetScreenByIdAsync(int id);
    Task<IEnumerable<ScreenResponseDto>> GetScreensByTheatreIdAsync(int theatreId);
    Task<ScreenResponseDto> CreateScreenAsync(CreateScreenDto dto);
    Task UpdateScreenAsync(int id, CreateScreenDto dto);
    Task DeleteScreenAsync(int id);
}