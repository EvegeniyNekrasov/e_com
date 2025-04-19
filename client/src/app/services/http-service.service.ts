import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class HttpServiceService {
  private http = inject(HttpClient);
  private cookie = inject(CookieService);

  get<T>(endpoint: string) {
    return this.http.get<T>(endpoint).pipe(
      catchError((error) => {
        console.error(error);
        return [];
      })
    );
  }

  post<T>(endpoint: string, data: any) {
    const token = this.getToken();
    const headers = token
      ? new HttpHeaders({
          Authorization: `Bearer ${token}`,
          'Content-Type': 'application/json',
        })
      : new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<T>(endpoint, data, { headers }).pipe(
      catchError((error) => {
        console.error(error);
        return [];
      })
    );
  }

  /**
   * This function set token receved from server to cookie
   * @param token token recived from server
   */
  setToken(token: string): void {
    this.cookie.set('token', token);
  }

  /**
   * This function return the token stored in the cookie
   * @returns string token
   */
  getToken(): string {
    return this.cookie.get('token');
  }

  /**
   *  This function delete the token stored in the cookie
   */
  deleteToken(): void {
    this.cookie.delete('token');
  }
}
