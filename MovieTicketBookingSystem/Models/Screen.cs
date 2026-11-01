using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class Screen
{
    [Key]
    public int ScreenId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int TheatreId { get; set; } // Foreign Key

    [ForeignKey("TheatreId")]
    public Theatre Theatre { get; set; } = null!;

    // Navigation property for seats (Uncommented now!)
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}