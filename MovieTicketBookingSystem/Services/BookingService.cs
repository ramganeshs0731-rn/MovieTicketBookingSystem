using MovieTicketBookingSystem.DTOs.Bookings;
using MovieTicketBookingSystem.Models;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services.Interfaces;

namespace MovieTicketBookingSystem.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IShowRepository _showRepository;

    public BookingService(IBookingRepository bookingRepository, IShowRepository showRepository)
    {
        _bookingRepository = bookingRepository;
        _showRepository = showRepository;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(MapToDto);
    }

    public async Task<BookingResponseDto?> GetBookingByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking == null ? null : MapToDto(booking);
    }

    public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
    {
        var show = await _showRepository.GetByIdAsync(dto.ShowId);
        if (show == null) throw new Exception("Show not found.");

        var seats = await _bookingRepository.GetSeatsByIdsAsync(dto.SeatIds);
        if (seats.Count != dto.SeatIds.Count) throw new Exception("One or more invalid seats selected.");

        // Check if show seats are already initialized, if not initialize them or check if booked
        var showSeats = await _bookingRepository.GetShowSeatsAsync(dto.ShowId, dto.SeatIds);

        foreach (var seatId in dto.SeatIds)
        {
            var showSeat = showSeats.FirstOrDefault(ss => ss.SeatId == seatId);
            if (showSeat != null && showSeat.IsBooked)
            {
                throw new Exception($"Seat ID {seatId} is already booked for this show.");
            }
        }

        // If show seats entries don't exist yet for these seats, create them
        foreach (var seatId in dto.SeatIds)
        {
            if (!showSeats.Any(ss => ss.SeatId == seatId))
            {
                var newShowSeat = new ShowSeat { ShowId = dto.ShowId, SeatId = seatId, IsBooked = true };
                // We can add them directly or handle via context
            }
        }

        // Mark seats as booked
        foreach (var ss in showSeats)
        {
            ss.IsBooked = true;
        }
        await _bookingRepository.UpdateShowSeatsAsync(showSeats);

        // Calculate total amount
        decimal totalAmount = show.TicketPrice * dto.SeatIds.Count;

        var booking = new Booking
        {
            ShowId = dto.ShowId,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            BookingTime = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Status = "Confirmed",
            BookingSeats = dto.SeatIds.Select(seatId => new BookingSeat { SeatId = seatId }).ToList()
        };

        await _bookingRepository.AddAsync(booking);

        var createdBooking = await _bookingRepository.GetByIdAsync(booking.BookingId);
        return MapToDto(createdBooking!);
    }

    private BookingResponseDto MapToDto(Booking b) => new()
    {
        BookingId = b.BookingId,
        ShowId = b.ShowId,
        MovieTitle = b.Show?.Movie?.Title ?? string.Empty,
        TheatreName = b.Show?.Screen?.Theatre?.Name ?? string.Empty,
        ScreenName = b.Show?.Screen?.Name ?? string.Empty,
        ShowTime = b.Show?.ShowTime ?? default,
        CustomerName = b.CustomerName,
        CustomerEmail = b.CustomerEmail,
        BookingTime = b.BookingTime,
        TotalAmount = b.TotalAmount,
        Status = b.Status,
        BookedSeatNumbers = b.BookingSeats?.Select(bs => bs.Seat?.SeatNumber ?? string.Empty).ToList() ?? new()
    };
}