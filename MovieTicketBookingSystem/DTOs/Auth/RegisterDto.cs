using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingSystem.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "Customer";
}