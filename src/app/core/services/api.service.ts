
import { Injectable, inject } from '@angular/core';

import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import {
  ApiMessage,
  Booking,
  LoginResponse,
  Movie,
  Screen,
  Seat,
  Show,
  Theatre
} from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private http = inject(HttpClient);

  private base = environment.apiUrl;

  private url(path: string): string {
    return `${this.base}/${path}`;
  }

  // ================= AUTH =================

  login(data: {
    email: string;
    password: string;
  }): Observable<LoginResponse> {

    return this.http.post<LoginResponse>(
      this.url('Auth/login'),
      data
    );
  }

  register(data: {
    username: string;
    email: string;
    password: string;
    role: string;
  }): Observable<ApiMessage> {

    return this.http.post<ApiMessage>(
      this.url('Auth/register'),
      data
    );
  }

  // ================= MOVIES =================

  movies(): Observable<Movie[]> {

    return this.http.get<Movie[]>(
      this.url('Movies')
    );
  }

  movie(id: number): Observable<Movie> {

    return this.http.get<Movie>(
      this.url(`Movies/${id}`)
    );
  }

  searchMovies(title: string): Observable<Movie[]> {

    const params = new HttpParams()
      .set('title', title);

    return this.http.get<Movie[]>(
      this.url('Movies/search'),
      { params }
    );
  }

  createMovie(data: Partial<Movie>): Observable<Movie> {

    return this.http.post<Movie>(
      this.url('Movies'),
      data
    );
  }

  updateMovie(
    id: number,
    data: Partial<Movie>
  ): Observable<unknown> {

    return this.http.put(
      this.url(`Movies/${id}`),
      data
    );
  }

  deleteMovie(id: number): Observable<unknown> {

    return this.http.delete(
      this.url(`Movies/${id}`)
    );
  }

  // ================= THEATRES =================

  theatres(): Observable<Theatre[]> {

    return this.http.get<Theatre[]>(
      this.url('Theatres')
    );
  }

  createTheatre(data: Partial<Theatre>): Observable<Theatre> {

    return this.http.post<Theatre>(
      this.url('Theatres'),
      data
    );
  }

  updateTheatre(
    id: number,
    data: Partial<Theatre>
  ): Observable<unknown> {

    return this.http.put(
      this.url(`Theatres/${id}`),
      data
    );
  }

  deleteTheatre(id: number): Observable<unknown> {

    return this.http.delete(
      this.url(`Theatres/${id}`)
    );
  }

  // ================= SCREENS =================

  screens(): Observable<Screen[]> {

    return this.http.get<Screen[]>(
      this.url('Screens')
    );
  }

  screensByTheatre(id: number): Observable<Screen[]> {

    return this.http.get<Screen[]>(
      this.url(`Screens/theatre/${id}`)
    );
  }

  createScreen(data: {
    name: string;
    theatreId: number;
  }): Observable<Screen> {

    return this.http.post<Screen>(
      this.url('Screens'),
      data
    );
  }

  updateScreen(
    id: number,
    data: {
      name: string;
      theatreId: number;
    }
  ): Observable<unknown> {

    return this.http.put(
      this.url(`Screens/${id}`),
      data
    );
  }

  deleteScreen(id: number): Observable<unknown> {

    return this.http.delete(
      this.url(`Screens/${id}`)
    );
  }

  // ================= SEATS =================

  seats(): Observable<Seat[]> {

    return this.http.get<Seat[]>(
      this.url('Seats')
    );
  }

  seatsByScreen(id: number): Observable<Seat[]> {

    return this.http.get<Seat[]>(
      this.url(`Seats/screen/${id}`)
    );
  }

  createSeat(data: {
    seatNumber: string;
    seatType: string;
    screenId: number;
  }): Observable<Seat> {

    return this.http.post<Seat>(
      this.url('Seats'),
      data
    );
  }

  updateSeat(
    id: number,
    data: {
      seatNumber: string;
      seatType: string;
      screenId: number;
    }
  ): Observable<unknown> {

    return this.http.put(
      this.url(`Seats/${id}`),
      data
    );
  }

  deleteSeat(id: number): Observable<unknown> {

    return this.http.delete(
      this.url(`Seats/${id}`)
    );
  }

  // ================= SHOWS =================

  shows(): Observable<Show[]> {

    return this.http.get<Show[]>(
      this.url('Shows')
    );
  }

  showsByMovie(id: number): Observable<Show[]> {

    return this.http.get<Show[]>(
      this.url(`Shows/movie/${id}`)
    );
  }

  createShow(data: {
    movieId: number;
    screenId: number;
    showTime: string;
    ticketPrice: number;
  }): Observable<Show> {

    return this.http.post<Show>(
      this.url('Shows'),
      data
    );
  }

  updateShow(
    id: number,
    data: {
      movieId: number;
      screenId: number;
      showTime: string;
      ticketPrice: number;
    }
  ): Observable<unknown> {

    return this.http.put(
      this.url(`Shows/${id}`),
      data
    );
  }

  deleteShow(id: number): Observable<unknown> {

    return this.http.delete(
      this.url(`Shows/${id}`)
    );
  }

  // ================= BOOKINGS =================

  bookings(): Observable<Booking[]> {

    return this.http.get<Booking[]>(
      this.url('Bookings')
    );
  }

  booking(id: number): Observable<Booking> {

    return this.http.get<Booking>(
      this.url(`Bookings/${id}`)
    );
  }

  createBooking(data: {
    showId: number;
    customerName: string;
    customerEmail: string;
    seatIds: number[];
  }): Observable<Booking> {

    return this.http.post<Booking>(
      this.url('Bookings'),
      data
    );
  }
}
