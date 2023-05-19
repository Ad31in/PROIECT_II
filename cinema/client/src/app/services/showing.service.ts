import { Injectable } from '@angular/core';
import { Showing } from '../models/showing';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Room } from '../models/room';

@Injectable({
  providedIn: 'root'
})
export class ShowingService {
  baseUrl = environment.apiUrl;
  showing: Showing | undefined;
  constructor(private http: HttpClient) { }


  addShowing(model: any): Observable<Showing> {
    return this.http.post<Showing>(this.baseUrl + "showing", model).pipe(
      map((showing) => showing)
    );
  }

  getShowingById(id: string): Observable<Showing> {
    return this.http.get<Showing>(this.baseUrl + "showing/" + id).pipe(
      map((showing) => {
        return showing;
      })
    )
  }

  getShowingsByMovieId(id: string): Observable<Showing[]> {
    return this.http.get<Showing[]>(this.baseUrl + "showing/movie/" + id).pipe(
      map((showings) => {
        return showings;
      })
    )
  }
}
