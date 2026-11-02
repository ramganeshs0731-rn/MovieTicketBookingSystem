using MovieTicketBookingSystem.DTOs.Bookings;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
    Task<BookingResponseDto?> GetBookingByIdAsync(int id);
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto);
}
