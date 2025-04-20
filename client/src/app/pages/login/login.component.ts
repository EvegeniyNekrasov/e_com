import { Component, inject, OnInit } from '@angular/core';
import { HttpServiceService } from '../../services/http-service.service';
import { Router } from '@angular/router';
import { FieldMetaLogin } from '../../types/auth.type';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly httpService = inject(HttpServiceService);
  userForm!: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.userForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  readonly fields: FieldMetaLogin[] = [
    { name: 'email', label: 'Email', type: 'text', placeholder: 'email' },
    {
      name: 'password',
      label: 'Password',
      type: 'password',
      placeholder: 'password',
    },
  ];

  ngOnInit(): void {  
    const isAuth = !!this.httpService.getToken();
    if (isAuth) {
      this.router.navigate(['/']);
    }
  }

  handleGoResiter() {
    this.router.navigate(['/register']);
  }

  login() {
    if (this.userForm.invalid) return;
    const { email, password } = this.userForm.value;

    this.httpService
      .post<any>('/api/Auth/Login', JSON.stringify({ email, password }))
      .subscribe((response) => {
        if (response) {
          const { token } = response;
          this.httpService.setToken(token);
          this.router.navigate(['/']);
        }
      });
  }
}
