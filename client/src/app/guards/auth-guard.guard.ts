import { CanActivate, Router } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { HttpServiceService } from '../services/http-service.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private readonly httpService = inject(HttpServiceService);
  private readonly router = inject(Router);

  canActivate(): boolean {
    const isAuth = !!this.httpService.getToken();
    if (!isAuth) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
