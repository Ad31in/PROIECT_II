import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../services/account.service';
import jwt_decode, { JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private accountService: AccountService) { }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        const token = user?.token;
        if (token) {
          const tokenPayload = jwt_decode<JwtPayload>(token) as { role: string };
          const isAdmin = tokenPayload.role;
          if (isAdmin == "Admin") {
            return true;
          } else {
            return false;
          }
        }
        return false;
      })
    );
  }
}
