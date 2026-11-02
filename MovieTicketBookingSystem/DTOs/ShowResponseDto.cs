namespace MovieTicketBookingSystem.DTOs.Shows;

public class ShowResponseDto
{
    public int ShowId { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
    public int ScreenId { get; set; }
    public string ScreenName { get; set; } = string.Empty;
    public string TheatreName { get; set; } = string.Empty;
    public DateTime ShowTime { get; set; }
    public decimal TicketPrice { get; set; }
}