import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import jwt_decode, { JwtPayload } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private toastr: ToastrService) {
    this.checkStorageChanges();
  }

  public checkStorageChanges() {
    window.addEventListener('storage', (event) => {
      if (event.storageArea === localStorage && event.key === 'user')
        if (event.newValue) {
          const user = JSON.parse(event.newValue) as User;
          this.currentUserSource.next(user);
        } else {
          this.currentUserSource.next(null);
        }
    });
  }


  login(model: any) {
    console.log(model);
    return this.http.post<User>(this.baseUrl + 'user/login', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'user/register', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  reload() {
    return this.http.get(this.baseUrl + 'user/reloadData').pipe(
      map(() => {
        this.toastr.success("Reloading information was successfully")
      })
    );
  }

  // TODO
  changePassword(model: any) {
    return this.http.put(this.baseUrl + 'users/change-password', model);
  }

  setCurrentUser(user: User) {
    const token = user.token;
    if (token) {
      const tokenPayload1 = jwt_decode<JwtPayload>(token) as { role: string };
      const admin = tokenPayload1.role;
      user.state = admin;
      const tokenPayload = jwt_decode<JwtPayload>(token) as { exp: number };
      const expirationDate = new Date(tokenPayload.exp * 1000);
      const timeout = expirationDate.getTime() - Date.now();
      setTimeout(() => this.logout(), timeout);
    }
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (!user) {
          return;
        }
      },
    });
  }
}
