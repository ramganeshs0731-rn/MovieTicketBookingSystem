using Microsoft.EntityFrameworkCore;
using MovieTicketBookingSystem.Data;
using MovieTicketBookingSystem.Middleware;
using MovieTicketBookingSystem.Repositories;
using MovieTicketBookingSystem.Repositories.Interfaces;
using MovieTicketBookingSystem.Services;
using MovieTicketBookingSystem.Services.Interfaces;
using Scalar.AspNetCore; // <-- Make sure this is imported

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. ADD THIS: Register the OpenAPI document generator
builder.Services.AddOpenApi();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Register Services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ITheatreService, TheatreService>();
builder.Services.AddScoped<IScreenService, ScreenService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// 2. ADD THIS: Expose the JSON spec and Scalar UI in Development mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();              // Generates /openapi/v1.json
    app.MapScalarApiReference();   // Maps the UI endpoint to /scalar/v1
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
