import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet, RouterLink, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { HttpServiceService } from './services/http-service.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  providers: [CookieService],
  templateUrl: './app.component.html',
})
export class AppComponent {
  private readonly httpService = inject(HttpServiceService);
  private readonly router = inject(Router);
  showHeader = () => !!this.httpService.getToken();

  logout() {
    this.httpService.deleteToken();
    this.router.navigate(['/login']);
  }
}
