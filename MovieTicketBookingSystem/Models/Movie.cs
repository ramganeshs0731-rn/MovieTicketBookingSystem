using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.Models;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public int DurationMinutes { get; set; }

    [Required]
    public DateOnly ReleaseDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Language { get; set; } = string.Empty; // Fixed dot to semicolon/comma syntax

    [Required]
    [MaxLength(50)]
    public string Genre { get; set; } = string.Empty;

    // Navigation property for shows
    public ICollection<Show> Shows { get; set; } = new List<Show>();
}