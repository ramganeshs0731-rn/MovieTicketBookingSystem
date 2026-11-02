using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.DTOs.Shows;

public class CreateShowDto
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    public int ScreenId { get; set; }

    [Required]
    public DateTime ShowTime { get; set; }

    [Required]
    [Range(0.01, 10000.00)]
    public decimal TicketPrice { get; set; }
}