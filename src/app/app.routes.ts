import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { HomeComponent } from './features/home/home.component';
import { LoginComponent } from './features/auth/login.component';
import { RegisterComponent } from './features/auth/register.component';
import { MovieDetailsComponent } from './features/movie-details/movie-details.component';
import { SeatSelectionComponent } from './features/seat-selection/seat-selection.component';
import { BookingsComponent } from './features/bookings/bookings.component';
import { AdminComponent } from './features/admin/admin.component';
export const routes: Routes = [
  { path: '', component: HomeComponent }, { path: 'login', component: LoginComponent }, { path: 'register', component: RegisterComponent },
  { path: 'movies/:id', component: MovieDetailsComponent }, { path: 'shows/:id/seats', component: SeatSelectionComponent, canActivate: [authGuard] },
  { path: 'my-bookings', component: BookingsComponent, canActivate: [authGuard] }, { path: 'admin', component: AdminComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '' }
];
