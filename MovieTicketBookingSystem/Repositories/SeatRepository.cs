using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;

namespace MovieTicketBookingSystem.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly ApplicationDbContext _context;

    public SeatRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetAllAsync() =>
        await _context.Seats.Include(s => s.Screen).ToListAsync();

    public async Task<Seat?> GetByIdAsync(int id) =>
        await _context.Seats.Include(s => s.Screen).FirstOrDefaultAsync(s => s.SeatId == id);

    public async Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId) =>
        await _context.Seats.Where(s => s.ScreenId == screenId).Include(s => s.Screen).ToListAsync();

    public async Task<Seat> AddAsync(Seat seat)
    {
        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();
        return seat;
    }

    public async Task AddRangeAsync(IEnumerable<Seat> seats)
    {
        _context.Seats.AddRange(seats);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var seat = await _context.Seats.FindAsync(id);
        if (seat != null)
        {
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}