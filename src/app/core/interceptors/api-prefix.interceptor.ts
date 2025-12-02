import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiPrefixInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if (!/^https?:\/\//i.test(request.url)) {
      const apiReq = request.clone({ url: `${environment.apiUrl}/${request.url.replace(/^\/+/,'')}` });
      return next.handle(apiReq);
    }
    return next.handle(request);
  }
  //can also add server down error
}
