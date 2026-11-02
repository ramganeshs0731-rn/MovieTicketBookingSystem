using MovieTicketBookingSystem.DTOs.Auth;

namespace MovieTicketBookingSystem.Services.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}