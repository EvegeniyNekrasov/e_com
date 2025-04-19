import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { HttpServiceService } from '../../services/http-service.service';

interface Register {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

@Component({
  selector: 'app-register',
  imports: [],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  private readonly router = inject(Router);
  private readonly httpService = inject(HttpServiceService);

  firstName = signal<string>('');
  lastName = signal<string>('');
  email = signal<string>('');
  password = signal<string>('');

  handleInput(e: Event, type: number): void {
    const value = (e.target as HTMLInputElement).value;
    const handler = {
      1: () => this.firstName.set(value),
      2: () => this.lastName.set(value),
      3: () => this.email.set(value),
      4: () => this.password.set(value),
    };

    handler[type as 1 | 2 | 3 | 4]();
  }

  handleGoLogin() {
    this.router.navigate(['/login']);
  }

  register() {
    // TODO: check if all data is not null
    // TODO: create and sender object
    // TODO: disbale register btn if no all data is writen
    // TODO: disbale register btn when make first http call, and enable it on response
    this.httpService
      .post<Register>(
        '/api/Auth/Register',
        JSON.stringify({
          firstName: this.firstName(),
          lastName: this.lastName(),
          email: this.email(),
          password: this.password(),
        })
      )
      .subscribe((d) => console.log(d));
  }
}
