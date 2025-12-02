import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ApiResponse } from '../models/api-response';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

   private unwrap<T>(obs: Observable<ApiResponse<T>>): Observable<T> {
    return obs.pipe(
      map(res => {
        // If backend uses wrapper but returns data inside data
        if (res && typeof res === 'object' && 'data' in res) {
          return res.data as T;
        }
        // If backend returned direct T (for backward compat)
        return (res as unknown) as T;
      }),
      catchError(err => {
        // rethrow for global interceptor to handle OR normalize
        return throwError(() => err);
      })
    );
  }

   get<T>(url: string, params?: HttpParams, headers?: HttpHeaders): Observable<T> {
    return this.unwrap<T>(
      this.http.get<ApiResponse<T>>(url, { params, headers })
    );
  }
   post<T>(url: string, body: any, headers?: HttpHeaders): Observable<T> {
    return this.unwrap<T>(this.http.post<ApiResponse<T>>(url, body, { headers }));
  }

  put<T>(url: string, body: any, headers?: HttpHeaders): Observable<T> {
    return this.unwrap<T>(this.http.put<ApiResponse<T>>(url, body, { headers }));
  }
   delete<T>(url: string, expectJson = false): Observable<T> {
    if (expectJson) {
      return this.unwrap<T>(this.http.delete<ApiResponse<T>>(url));
    } else {
      // if backend returns 204 or empty body, request it as text to avoid parse error
      return this.http.delete(url, { responseType: 'text' }).pipe(
        map(txt => {
          // try to parse JSON if backend sends JSON string
          try {
            const json = JSON.parse(txt);
            // if generic wrapper, return json.data
            if (json && typeof json === 'object' && 'data' in json) return json.data as T;
            return json as T;
          } catch {
            // empty body (204) or plain text — return a null/undefined value for caller
            return null as unknown as T;
          }
        }),
        catchError(err => throwError(() => err))
      );
    }
  }
  
}
