using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Middleware;
using MovieTicketBookingSystem.Repositories;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services;
using MovieTicketBookingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured.");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true
    };
});

// Register Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register Repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ITheatreRepository, TheatreRepository>();
builder.Services.AddScoped<IScreenRepository, ScreenRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ITheatreService, TheatreService>();
builder.Services.AddScoped<IScreenService, ScreenService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseHttpsRedirection();

// Must be placed BEFORE UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
