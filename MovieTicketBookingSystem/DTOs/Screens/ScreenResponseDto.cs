namespace MovieTicketBookingSystem.DTOs.Screens;

public class ScreenResponseDto
{
    public int ScreenId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TheatreId { get; set; }
    public string TheatreName { get; set; } = string.Empty;
}