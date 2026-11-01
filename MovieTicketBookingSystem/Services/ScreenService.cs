using MovieTicketBookingSystem.DTOs.Screens;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _screenRepository;
    private readonly ITheatreRepository _theatreRepository;

    public ScreenService(IScreenRepository screenRepository, ITheatreRepository theatreRepository)
    {
        _screenRepository = screenRepository;
        _theatreRepository = theatreRepository;
    }

    public async Task<IEnumerable<ScreenResponseDto>> GetAllScreensAsync()
    {
        var screens = await _screenRepository.GetAllAsync();
        return screens.Select(s => new ScreenResponseDto
        {
            ScreenId = s.ScreenId,
            Name = s.Name,
            TheatreId = s.TheatreId,
            TheatreName = s.Theatre?.Name ?? string.Empty
        });
    }

    public async Task<ScreenResponseDto?> GetScreenByIdAsync(int id)
    {
        var s = await _screenRepository.GetByIdAsync(id);
        if (s == null) return null;

        return new ScreenResponseDto
        {
            ScreenId = s.ScreenId,
            Name = s.Name,
            TheatreId = s.TheatreId,
            TheatreName = s.Theatre?.Name ?? string.Empty
        };
    }

    public async Task<IEnumerable<ScreenResponseDto>> GetScreensByTheatreIdAsync(int theatreId)
    {
        var screens = await _screenRepository.GetByTheatreIdAsync(theatreId);
        return screens.Select(s => new ScreenResponseDto
        {
            ScreenId = s.ScreenId,
            Name = s.Name,
            TheatreId = s.TheatreId,
            TheatreName = s.Theatre?.Name ?? string.Empty
        });
    }

    public async Task<ScreenResponseDto> CreateScreenAsync(CreateScreenDto dto)
    {
        // Check if theatre exists
        var theatre = await _theatreRepository.GetByIdAsync(dto.TheatreId);
        if (theatre == null) throw new Exception("Theatre not found.");

        var screen = new Screen
        {
            Name = dto.Name,
            TheatreId = dto.TheatreId
        };

        await _screenRepository.AddAsync(screen);

        return new ScreenResponseDto
        {
            ScreenId = screen.ScreenId,
            Name = screen.Name,
            TheatreId = screen.TheatreId,
            TheatreName = theatre.Name
        };
    }

    public async Task UpdateScreenAsync(int id, CreateScreenDto dto)
    {
        var screen = await _screenRepository.GetByIdAsync(id);
        if (screen != null)
        {
            screen.Name = dto.Name;
            screen.TheatreId = dto.TheatreId;
            await _screenRepository.UpdateAsync(screen);
        }
    }

    public async Task DeleteScreenAsync(int id)
    {
        await _screenRepository.DeleteAsync(id);
    }
}