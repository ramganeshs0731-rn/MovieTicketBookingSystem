using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> GetByCustomerEmailAsync(string email);
    Task<Booking> AddAsync(Booking booking);
    Task<List<Seat>> GetSeatsByIdsAsync(List<int> seatIds);
    Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds);
    Task UpdateShowSeatsAsync(List<ShowSeat> showSeats);
}