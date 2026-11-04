import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/services/api.service';
import { Movie } from '../../core/models/models';

@Component({ standalone: true, imports: [CommonModule, RouterLink, FormsModule], template: `
<section class="hero"><div class="hero-copy"><div class="eyebrow"><span></span> YOUR NEXT CINEMA MOMENT</div><h1>Stories worth<br><em>watching.</em></h1><p>Find the films you love. Pick your seats. Make it a night to remember.</p><a class="primary-btn" href="#movies">Explore movies <b>↗</b></a><div class="hero-meta"><span>✦ CURATED FOR YOU</span><span>·</span><span>BOOK IN MINUTES</span></div></div><div class="hero-art"><div class="orbit orbit-one"></div><div class="orbit orbit-two"></div><div class="hero-poster"><div class="poster-glow"></div><div class="poster-content"><span>THE BIG SCREEN</span><strong>YOUR<br>NEXT<br>STORY.</strong><small>IS WAITING</small></div></div><div class="floating-note"><span class="note-icon">▶</span><div><b>Lights. Camera.</b><small>Your moment starts here.</small></div></div></div></section>
<section id="movies" class="section"><div class="section-heading"><div><div class="eyebrow">NOW SHOWING</div><h2>Find your <em>film.</em></h2></div><div class="search-wrap"><span>⌕</span><input [(ngModel)]="query" (ngModelChange)="filterMovies()" placeholder="Search movies..." aria-label="Search movies"></div></div>
<div *ngIf="loading" class="state-panel">Loading movies from your cinema...</div><div *ngIf="error" class="state-panel error">{{error}} <button class="text-btn" (click)="loadMovies()">Try again</button></div>
<div class="movie-grid" *ngIf="!loading && filtered.length"><a class="movie-card" *ngFor="let movie of filtered; let i = index" [routerLink]="['/movies', movie.movieId]"><div class="poster" [style.background]="posterBackground(i)"><span class="poster-number">{{('0'+(i+1)).slice(-2)}}</span><div class="poster-title">{{movie.title}}</div><div class="poster-sub">{{movie.genre}} · {{movie.language}}</div><span class="poster-cta">VIEW FILM ↗</span></div><div class="movie-info"><div><h3>{{movie.title}}</h3><p>{{movie.durationMinutes}} min <span>·</span> {{movie.language}}</p></div><span class="arrow-circle">↗</span></div></a></div>
<div class="state-panel" *ngIf="!loading && !filtered.length">No movies found. Try another title or add movies through your backend.</div></section>
<section class="strip"><div><span class="strip-icon">✳</span><div><b>Your seat. Your screen. Your story.</b><small>A smoother way to plan your next movie night.</small></div></div><span class="strip-right">MADE FOR MOVIE PEOPLE&nbsp; ↗</span></section>` })
export class HomeComponent implements OnInit {
  private api = inject(ApiService); movies: Movie[] = []; filtered: Movie[] = []; query = ''; loading = true; error = '';
  ngOnInit(): void { this.loadMovies(); }
  loadMovies(): void { this.loading = true; this.error = ''; this.api.movies().subscribe({ next: data => { this.movies = data ?? []; this.filterMovies(); this.loading = false; }, error: err => { this.error = this.message(err, 'Could not load movies. Check that the backend is running.'); this.loading = false; } }); }
  filterMovies(): void { const q = this.query.trim().toLowerCase(); this.filtered = this.movies.filter(m => m.title.toLowerCase().includes(q) || m.genre.toLowerCase().includes(q) || m.language.toLowerCase().includes(q)); }
  posterBackground(i: number): string { const gradients = ['linear-gradient(145deg,#382b4b,#bd5a45 55%,#f0b36a)','linear-gradient(145deg,#152d3d,#286b72 55%,#d4a36b)','linear-gradient(145deg,#29253e,#6e4d82 55%,#d68c74)','linear-gradient(145deg,#263a31,#8d9d65 55%,#d6b67a)','linear-gradient(145deg,#352b2a,#9b5142 55%,#d5a16c)']; return gradients[i % gradients.length]; }
  private message(err: any, fallback: string): string { return err?.error?.message || err?.message || fallback; }
}
