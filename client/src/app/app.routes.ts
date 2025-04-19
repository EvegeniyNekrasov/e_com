import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth-guard.guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./pages/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'blog',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./pages/blog/blog.component').then((m) => m.BlogComponent),
  },
  {
    path: 'login',
    pathMatch: 'full',
    loadComponent: () =>
      import('./pages/login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'register',
    pathMatch: 'full',
    loadComponent: () =>
      import('./pages/register/register.component').then(
        (m) => m.RegisterComponent
      ),
  },
];
