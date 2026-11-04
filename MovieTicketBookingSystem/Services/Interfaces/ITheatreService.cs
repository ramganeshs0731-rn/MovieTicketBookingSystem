using MovieTicketBookingSystem.DTOs.Theatres;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface ITheatreService
{
    Task<IEnumerable<TheatreResponseDto>> GetAllTheatresAsync();
    Task<TheatreResponseDto?> GetTheatreByIdAsync(int id);
    Task<TheatreResponseDto> CreateTheatreAsync(CreateTheatreDto createDto);
    Task UpdateTheatreAsync(int id, CreateTheatreDto updateDto);
    Task DeleteTheatreAsync(int id);
}