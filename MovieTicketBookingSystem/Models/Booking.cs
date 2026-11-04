using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingSystem.Models;

public class Booking
{
    [Key]
    public int BookingId { get; set; }

    [Required]
    public int ShowId { get; set; }
    [ForeignKey("ShowId")]
    public Show Show { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    public DateTime BookingTime { get; set; } = DateTime.UtcNow;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Confirmed"; // Confirmed, Cancelled

    // Navigation properties
    public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
}