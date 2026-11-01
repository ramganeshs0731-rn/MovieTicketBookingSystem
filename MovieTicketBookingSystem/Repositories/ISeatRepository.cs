using MovieTicketBookingSystem.Models;

namespace MovieTicketBookingSystem.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<IEnumerable<Seat>> GetAllAsync();
    Task<Seat?> GetByIdAsync(int id);
    Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId);
    Task<Seat> AddAsync(Seat seat);
    Task AddRangeAsync(IEnumerable<Seat> seats); // Helpful for bulk seat creation
    Task UpdateAsync(Seat seat);
    Task DeleteAsync(int id);
}