import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpServiceService {
  private http = inject(HttpClient);

  get<T>(endpoint: string) {
    return this.http.get<T>(endpoint).pipe(
      catchError((error) => {
        console.error(error);
        return [];
      })
    );
  }
}
