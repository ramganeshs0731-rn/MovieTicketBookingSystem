namespace MovieTicketBookingSystem.DTOs.Bookings;

public class BookingResponseDto
{
    public int BookingId { get; set; }
    public int ShowId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public string TheatreName { get; set; } = string.Empty;
    public string ScreenName { get; set; } = string.Empty;
    public DateTime ShowTime { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> BookedSeatNumbers { get; set; } = new();
}