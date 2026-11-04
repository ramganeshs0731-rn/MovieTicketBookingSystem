using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;

namespace MovieTicketBookingSystem.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _context;

    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync() =>
        await _context.Bookings
            .Include(b => b.Show)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Show)
                .ThenInclude(s => s.Screen)
                    .ThenInclude(sc => sc.Theatre)
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .ToListAsync();

    public async Task<Booking?> GetByIdAsync(int id) =>
        await _context.Bookings
            .Include(b => b.Show)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Show)
                .ThenInclude(s => s.Screen)
                    .ThenInclude(sc => sc.Theatre)
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .FirstOrDefaultAsync(b => b.BookingId == id);

    public async Task<IEnumerable<Booking>> GetByCustomerEmailAsync(string email) =>
        await _context.Bookings
            .Where(b => b.CustomerEmail == email)
            .Include(b => b.Show)
                .ThenInclude(s => s.Movie)
            .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
            .ToListAsync();

    public async Task<List<Seat>> GetSeatsByIdsAsync(List<int> seatIds) =>
        await _context.Seats.Where(s => seatIds.Contains(s.SeatId)).ToListAsync();

    public async Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds) =>
        await _context.ShowSeats
            .Where(ss => ss.ShowId == showId && seatIds.Contains(ss.SeatId))
            .ToListAsync();

    public async Task UpdateShowSeatsAsync(List<ShowSeat> showSeats)
    {
        _context.ShowSeats.UpdateRange(showSeats);
        await _context.SaveChangesAsync();
    }

    public async Task<Booking> AddAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }
}