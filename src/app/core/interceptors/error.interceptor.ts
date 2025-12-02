import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {

        let message = "Something went wrong.";
        let backend = err.error;

        // ----------------------------
        // CASE 1: Backend returned STRING (JSON text)
        // ----------------------------
        if (typeof backend === "string") {
          try {
            backend = JSON.parse(backend);  // convert to object
          } catch {
            // Not a JSON → show directly
            window.alert(backend);
            return throwError(() => err);
          }
        }

        // ----------------------------
        // CASE 2: Backend returned OBJECT
        // ----------------------------
        if (backend && typeof backend === "object") {

          // ResponseWrapper { success, message }
          if (backend.message) {
            message = backend.message;
          }

          // .NET problemDetails
          else if (backend.title) {
            message = backend.title;
          }
        }

        // ----------------------------
        // CASE 3: No backend body → fallback
        // ----------------------------
        if (!backend) {
          message = err.statusText || message;
        }

        window.alert(message);
        return throwError(() => err);
      })
    );
  }
}
