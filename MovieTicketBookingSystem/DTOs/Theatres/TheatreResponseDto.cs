namespace MovieTicketBookingSystem.DTOs.Theatres;

public class TheatreResponseDto
{
    public int TheatreId { get; set; } // Make sure 'public' is added here!
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}