# CineBook — Angular frontend for Movie Ticket Booking System

This frontend was prepared against the uploaded `MovieTicketBookingSystem` ASP.NET Core API source.

## Requirements
- Node.js 20.19+ or 22.12+
- npm
- Angular CLI is optional (the npm scripts use the local CLI)
- Backend running at `https://localhost:7185` or `http://localhost:5033`

## Run the frontend

```powershell
cd mtbs-frontend
npm install
npm start
```

Open http://localhost:4200.

Build for production:

```powershell
npm run build
```

## Backend URL

Edit `src/environments/environment.ts` if your API uses another address:

```ts
apiUrl: 'https://localhost:7185/api'
```

The uploaded backend `Properties/launchSettings.json` specifies HTTPS `7185` and HTTP `5033`.

## Backend CORS requirement

The uploaded `Program.cs` does not register a CORS policy. Add this before `var app = builder.Build();`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
```

Then add this to the HTTP pipeline before authentication/authorization:

```csharp
app.UseCors("AngularClient");
```

If using the HTTPS Angular origin, include that exact origin as well.

## Included pages
- Discover / movie catalogue with search
- Movie details and showtime list
- Customer registration and login
- JWT token storage and HTTP authorization interceptor
- Route protection for booking, booking history and management pages
- Seat selection and booking submission
- Booking history
- Admin-style CRUD screens for Movies, Theatres, Screens, Seats and Shows; booking list

## Important integration notes from the supplied backend

1. `POST /api/Auth/register` accepts `username`, `email`, `password`, and `role`. The frontend always submits `role: "Customer"`.
2. `POST /api/Auth/login` returns `{ "token": "..." }`.
3. Booking endpoints require a JWT. The interceptor sends `Authorization: Bearer <token>`.
4. The API has no endpoint to return per-show seat availability. The seat page loads seats by screen and allows provisional selection; the booking API is the final validation point. Do not represent this as live seat availability.
5. The API has no payment endpoint. Booking confirmation is not an online payment.
6. The supplied backend does not apply role-based authorization to management endpoints. The Angular `/admin` route only checks that a user is signed in; it is not a security boundary. Add server-side role authorization before deployment.
7. The supplied `BookingService` creates missing `ShowSeat` objects but does not persist them, and only updates existing show-seat rows. Fix this in the backend before relying on seat locking/concurrency.
8. The supplied `BookingsController` returns all bookings to any authenticated user. Add user ownership filtering in the backend before production use.

## Project layout

```text
src/app/
  core/
    guards/          route guards
    interceptors/    JWT HTTP interceptor
    models/          API contracts
    services/        API and authentication services
  features/
    home/            movie catalogue
    auth/            login and registration
    movie-details/   movie details and shows
    seat-selection/  seat selection and booking
    bookings/        booking history
    admin/           management CRUD UI
  app.component.ts
  app.routes.ts
  app.config.ts
```

## Troubleshooting
- **CORS error:** apply the CORS setup above and restart the backend.
- **Certificate warning / `ERR_CERT_AUTHORITY_INVALID`:** open `https://localhost:7185` in your browser and trust the local ASP.NET development certificate, or use `http://localhost:5033` in `environment.ts` while developing.
- **401 on booking:** sign in again; the JWT expires according to backend `DurationInMinutes`.
- **No movie cards:** create movie records through the API/management screen first.
- **No showtimes:** create shows linked to an existing movie and screen.
