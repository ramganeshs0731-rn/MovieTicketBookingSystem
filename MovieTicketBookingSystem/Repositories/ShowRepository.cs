using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;

namespace MovieTicketBookingSystem.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly ApplicationDbContext _context;

    public ShowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Show>> GetAllAsync() =>
        await _context.Shows
            .Include(s => s.Movie)
            .Include(s => s.Screen)
            .ThenInclude(sc => sc.Theatre)
            .ToListAsync();

    public async Task<Show?> GetByIdAsync(int id) =>
        await _context.Shows
            .Include(s => s.Movie)
            .Include(s => s.Screen)
            .ThenInclude(sc => sc.Theatre)
            .FirstOrDefaultAsync(s => s.ShowId == id);

    public async Task<IEnumerable<Show>> GetByMovieIdAsync(int movieId) =>
        await _context.Shows
            .Where(s => s.MovieId == movieId)
            .Include(s => s.Movie)
            .Include(s => s.Screen)
            .ThenInclude(sc => sc.Theatre)
            .ToListAsync();

    public async Task<IEnumerable<Show>> GetByScreenIdAsync(int screenId) =>
        await _context.Shows
            .Where(s => s.ScreenId == screenId)
            .Include(s => s.Movie)
            .Include(s => s.Screen)
            .ThenInclude(sc => sc.Theatre)
            .ToListAsync();

    public async Task<Show> AddAsync(Show show)
    {
        _context.Shows.Add(show);
        await _context.SaveChangesAsync();
        return show;
    }

    public async Task UpdateAsync(Show show)
    {
        _context.Shows.Update(show);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var show = await _context.Shows.FindAsync(id);
        if (show != null)
        {
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
        }
    }
}