import { Injectable, computed, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly key = 'mtbs_access_token';
  readonly token = signal<string | null>(localStorage.getItem(this.key));
  readonly isLoggedIn = computed(() => !!this.token());
  login(token: string): void { localStorage.setItem(this.key, token); this.token.set(token); }
  logout(): void { localStorage.removeItem(this.key); this.token.set(null); }
  getToken(): string | null { return this.token(); }
  getClaims(): Record<string, unknown> {
    const token = this.token(); if (!token) return {};
    try { const payload = token.split('.')[1]; return JSON.parse(atob(payload.replace(/-/g, '+').replace(/_/g, '/'))) as Record<string, unknown>; }
    catch { return {}; }
  }
  getUserName(): string { const c = this.getClaims(); return String(c['unique_name'] ?? c['name'] ?? ''); }
  getUserEmail(): string { const c = this.getClaims(); return String(c['email'] ?? ''); }
  getRole(): string { const c = this.getClaims(); return String(c['role'] ?? ''); }
}
