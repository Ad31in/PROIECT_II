import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable} from 'rxjs';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService, private router: Router) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (localStorage.getItem('user')) {
      const user = JSON.parse(localStorage.getItem('user') || '{}');
      if (user && user.token) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${user.token}`,
          },
        });
      } else {
        this.accountService.logout();
        this.router.navigateByUrl('');
      }
    }
    return next.handle(request);
  }
}
