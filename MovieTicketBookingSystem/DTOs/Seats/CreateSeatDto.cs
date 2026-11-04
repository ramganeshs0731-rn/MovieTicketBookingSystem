using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.DTOs.Seats;

public class CreateSeatDto
{
    [Required]
    [MaxLength(10)]
    public string SeatNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string SeatType { get; set; } = "Normal";

    [Required]
    public int ScreenId { get; set; }
}