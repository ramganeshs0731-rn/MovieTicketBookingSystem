
export interface Movie {
  movieId: number;
  title: string;
  description?: string | null;
  durationMinutes: number;
  releaseDate: string;
  language: string;
  genre: string;
}

export interface Theatre {
  theatreId: number;
  name: string;
  location: string;
  city: string;
}

export interface Screen {
  screenId: number;
  name: string;
  theatreId: number;
  theatreName: string;
}

export interface Seat {
  seatId: number;
  seatNumber: string;
  seatType: string;
  screenId: number;
  screenName: string;
}

export interface Show {
  showId: number;
  movieId: number;
  movieTitle: string;
  screenId: number;
  screenName: string;
  theatreName: string;
  showTime: string;
  ticketPrice: number;
}

export interface Booking {
  bookingId: number;
  showId: number;
  movieTitle: string;
  theatreName: string;
  screenName: string;
  showTime: string;
  customerName: string;
  customerEmail: string;
  bookingTime: string;
  totalAmount: number;
  status: string;
  bookedSeatNumbers: string[];
}

export interface LoginResponse {
  token: string;
}

export interface ApiMessage {
  message: string;
}
