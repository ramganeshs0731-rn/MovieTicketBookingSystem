using MovieTicketBookingSystem.DTOs.Seats;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<SeatResponseDto>> GetAllSeatsAsync();
    Task<SeatResponseDto?> GetSeatByIdAsync(int id);
    Task<IEnumerable<SeatResponseDto>> GetSeatsByScreenIdAsync(int screenId);
    Task<SeatResponseDto> CreateSeatAsync(CreateSeatDto dto);
    Task UpdateSeatAsync(int id, CreateSeatDto dto);
    Task DeleteSeatAsync(int id);
}