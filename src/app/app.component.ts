import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth.service';

@Component({ selector: 'app-root', standalone: true, imports: [RouterOutlet, RouterLink, RouterLinkActive], template: `
<header class="topbar"><a class="brand" routerLink="/"><span class="brand-mark">C</span> CINE<span>BOOK</span></a><nav><a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Discover</a><a routerLink="/my-bookings" routerLinkActive="active" [hidden]="!auth.isLoggedIn()">My bookings</a><a routerLink="/admin" routerLinkActive="active" [hidden]="!auth.isLoggedIn()">Manage</a></nav><div class="nav-actions"><span class="welcome" [hidden]="!auth.isLoggedIn()">{{auth.getUserName()}}</span><a class="login-link" routerLink="/login" [hidden]="auth.isLoggedIn()">Sign in</a><a class="primary-btn small" routerLink="/register" [hidden]="auth.isLoggedIn()">Get started</a><button class="ghost-btn" [hidden]="!auth.isLoggedIn()" (click)="logout()">Sign out</button></div></header>
<main><router-outlet /></main><footer class="footer"><a class="brand" routerLink="/">CINE<span>BOOK</span></a><span>Movies are better on the big screen.</span><span>© 2026 CineBook</span></footer>` })
export class AppComponent { auth = inject(AuthService); private router = inject(Router); logout(): void { this.auth.logout(); this.router.navigate(['/']); } }
