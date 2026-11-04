using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.DTOs.Screens;

public class CreateScreenDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int TheatreId { get; set; }
}