import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpServiceService } from '../../services/http-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly httpService = inject(HttpServiceService);

  email = signal<string>('');
  pswd = signal<string>('');

  ngOnInit(): void {
    const isAuth = !!this.httpService.getToken();
    if (isAuth) {
      this.router.navigate(['/']);
    }
  }

  handleInput(e: Event, type: number): void {
    const value = (e.target as HTMLInputElement).value;
    const handler = {
      1: () => this.email.set(value),
      2: () => this.pswd.set(value),
    };

    handler[type as 1 | 2]();
  }

  handleGoResiter() {
    this.router.navigate(['/register']);
  }

  login() {
    if (this.email() !== '' && this.pswd() !== '') {
      const data = JSON.stringify({
        email: this.email(),
        password: this.pswd(),
      });

      this.httpService
        .post<any>('/api/Auth/Login', data)
        .subscribe((response) => {
          if (response) {
            const { token } = response;
            this.httpService.setToken(token);
            this.router.navigate(['/']);
          }
        });
    }
  }
}
