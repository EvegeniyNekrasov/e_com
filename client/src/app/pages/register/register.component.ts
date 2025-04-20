import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpServiceService } from '../../services/http-service.service';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';

interface Response {
  id: string;
}

interface FieldMeta {
  name: 'firstName' | 'lastName' | 'email' | 'password';
  label: string;
  type: string;
  placeholder: string;
}

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  private readonly router = inject(Router);
  private readonly httpService = inject(HttpServiceService);
  userForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  readonly fields: FieldMeta[] = [
    {
      name: 'firstName',
      label: 'First name',
      type: 'text',
      placeholder: 'first name',
    },
    {
      name: 'lastName',
      label: 'Last name',
      type: 'text',
      placeholder: 'last name',
    },
    { name: 'email', label: 'Email', type: 'text', placeholder: 'email' },
    {
      name: 'password',
      label: 'Password',
      type: 'password',
      placeholder: 'password',
    },
  ];

  handleGoLogin() {
    this.router.navigate(['/login']);
  }

  register() {
    if (this.userForm.invalid) return;
    const { firstName, lastName, email, password } = this.userForm.value;
    this.httpService
      .post<Response>(
        '/api/Auth/Register',
        JSON.stringify({
          firstName,
          lastName,
          email,
          password,
        })
      )
      .subscribe((response) => {
        if (response.id) this.router.navigate(['/login']);
      });
  }
}
