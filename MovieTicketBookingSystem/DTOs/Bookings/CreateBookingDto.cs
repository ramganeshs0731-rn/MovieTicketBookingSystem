using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.DTOs.Bookings;

public class CreateBookingDto
{
    [Required]
    public int ShowId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    public List<int> SeatIds { get; set; } = new(); // List of seats being booked
}