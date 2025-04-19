import { Component, inject, signal } from '@angular/core';
import { HttpServiceService } from '../../services/http-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  constructor(private router: Router) {}

  http = inject(HttpServiceService);

  email = signal<string>('');
  pswd = signal<string>('');

  handleInput(e: Event, type: number): void {
    const value = (e.target as HTMLInputElement).value;
    const handler = {
      1: () => this.email.set(value),
      2: () => this.pswd.set(value),
    };

    handler[type as 1 | 2]();
  }

  login() {
    if (this.email() !== '' && this.pswd() !== '') {
      const data = JSON.stringify({
        email: this.email(),
        password: this.pswd(),
      });

      this.http.post<any>('/api/Auth/Login', data).subscribe((response) => {
        if (response) {
          const { token } = response;
          this.http.setToken(token);
          localStorage.setItem('token', JSON.stringify({ token }));
          // this.router.navigate(["/"]);
        }
      });
    }
  }
}
