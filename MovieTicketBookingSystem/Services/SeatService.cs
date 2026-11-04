using MovieTicketBookingSystem.DTOs.Seats;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IScreenRepository _screenRepository;

    public SeatService(ISeatRepository seatRepository, IScreenRepository screenRepository)
    {
        _seatRepository = seatRepository;
        _screenRepository = screenRepository;
    }

    public async Task<IEnumerable<SeatResponseDto>> GetAllSeatsAsync()
    {
        var seats = await _seatRepository.GetAllAsync();
        return seats.Select(s => new SeatResponseDto
        {
            SeatId = s.SeatId,
            SeatNumber = s.SeatNumber,
            SeatType = s.SeatType,
            ScreenId = s.ScreenId,
            ScreenName = s.Screen?.Name ?? string.Empty
        });
    }

    public async Task<SeatResponseDto?> GetSeatByIdAsync(int id)
    {
        var s = await _seatRepository.GetByIdAsync(id);
        if (s == null) return null;

        return new SeatResponseDto
        {
            SeatId = s.SeatId,
            SeatNumber = s.SeatNumber,
            SeatType = s.SeatType,
            ScreenId = s.ScreenId,
            ScreenName = s.Screen?.Name ?? string.Empty
        };
    }

    public async Task<IEnumerable<SeatResponseDto>> GetSeatsByScreenIdAsync(int screenId)
    {
        var seats = await _seatRepository.GetByScreenIdAsync(screenId);
        return seats.Select(s => new SeatResponseDto
        {
            SeatId = s.SeatId,
            SeatNumber = s.SeatNumber,
            SeatType = s.SeatType,
            ScreenId = s.ScreenId,
            ScreenName = s.Screen?.Name ?? string.Empty
        });
    }

    public async Task<SeatResponseDto> CreateSeatAsync(CreateSeatDto dto)
    {
        var screen = await _screenRepository.GetByIdAsync(dto.ScreenId);
        if (screen == null) throw new Exception("Screen not found.");

        var seat = new Seat
        {
            SeatNumber = dto.SeatNumber,
            SeatType = dto.SeatType,
            ScreenId = dto.ScreenId
        };

        await _seatRepository.AddAsync(seat);

        return new SeatResponseDto
        {
            SeatId = seat.SeatId,
            SeatNumber = seat.SeatNumber,
            SeatType = seat.SeatType,
            ScreenId = seat.ScreenId,
            ScreenName = screen.Name
        };
    }

    public async Task UpdateSeatAsync(int id, CreateSeatDto dto)
    {
        var seat = await _seatRepository.GetByIdAsync(id);
        if (seat != null)
        {
            seat.SeatNumber = dto.SeatNumber;
            seat.SeatType = dto.SeatType;
            seat.ScreenId = dto.ScreenId;
            await _seatRepository.UpdateAsync(seat);
        }
    }

    public async Task DeleteSeatAsync(int id)
    {
        await _seatRepository.DeleteAsync(id);
    }
}