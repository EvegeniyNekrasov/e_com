import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpServiceService } from './services/http-service.service';

interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  createdAt: string;
}

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'client';
  http = inject(HttpServiceService);

  ngOnInit(): void {
    this.http
      .get<User[]>('/api/User/GetUsers')
      .subscribe((i) => console.log(i));
  }
}
