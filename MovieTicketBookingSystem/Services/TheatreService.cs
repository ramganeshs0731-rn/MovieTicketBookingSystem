using MovieTicketBookingSystem.DTOs.Theatres;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class TheatreService : ITheatreService
{
    private readonly ITheatreRepository _theatreRepository;

    public TheatreService(ITheatreRepository theatreRepository)
    {
        _theatreRepository = theatreRepository;
    }

    public async Task<IEnumerable<TheatreResponseDto>> GetAllTheatresAsync()
    {
        var theatres = await _theatreRepository.GetAllAsync();
        return theatres.Select(t => new TheatreResponseDto
        {
            TheatreId = t.TheatreId,
            Name = t.Name,
            Location = t.Location,
            City = t.City
        });
    }

    public async Task<TheatreResponseDto?> GetTheatreByIdAsync(int id)
    {
        var t = await _theatreRepository.GetByIdAsync(id);
        if (t == null) return null;

        return new TheatreResponseDto
        {
            TheatreId = t.TheatreId,
            Name = t.Name,
            Location = t.Location,
            City = t.City
        };
    }

    public async Task<TheatreResponseDto> CreateTheatreAsync(CreateTheatreDto dto)
    {
        var theatre = new Theatre
        {
            Name = dto.Name,
            Location = dto.Location,
            City = dto.City
        };

        await _theatreRepository.AddAsync(theatre);

        return new TheatreResponseDto
        {
            TheatreId = theatre.TheatreId,
            Name = theatre.Name,
            Location = theatre.Location,
            City = theatre.City
        };
    }

    public async Task UpdateTheatreAsync(int id, CreateTheatreDto dto)
    {
        var theatre = await _theatreRepository.GetByIdAsync(id);
        if (theatre != null)
        {
            theatre.Name = dto.Name;
            theatre.Location = dto.Location;
            theatre.City = dto.City;
            await _theatreRepository.UpdateAsync(theatre);
        }
    }

    public async Task DeleteTheatreAsync(int id)
    {
        await _theatreRepository.DeleteAsync(id);
    }
}