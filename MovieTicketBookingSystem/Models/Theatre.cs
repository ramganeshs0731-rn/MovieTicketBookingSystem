using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.Models;

public class Theatre
{
    [Key]
    public int TheatreId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(50)]
    public string City { get; set; } = string.Empty;

    // Navigation property for screens (Temporarily commented until Screen model is created)
    // public ICollection<Screen> Screens { get; set; } = new List<Screen>();
}