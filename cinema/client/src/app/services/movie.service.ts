import { Injectable } from '@angular/core';
import { Movie } from '../models/movie';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  baseUrl = environment.apiUrl;
  movies: Movie[] = [];
  movie: Movie | undefined;
  constructor(private http: HttpClient) { }

  getMovies() {
    return this.http.get<Movie[]>(this.baseUrl + "movie").pipe(
      map((movies) => {
        this.movies = movies;
        return movies;
      })
    )
  }

  uploadPhoto(movieId: string, file: File): Observable<Movie> {
    const formData: FormData = new FormData();
    formData.append('file', file);

    return this.http.post<Movie>(`${this.baseUrl}movie/${movieId}/addphoto`, formData).pipe(
      map((movie) => movie)
    );
  }

  addMovie(model: any): Observable<Movie> {
    return this.http.post<Movie>(this.baseUrl + "movie", model).pipe(
      map((movie) => movie)
    );
  }

  getMovieById(id: string): Observable<Movie> {
    return this.http.get<Movie>(this.baseUrl + "movie/" + id).pipe(
      map((movie) => {
        return movie;
      })
    )
  }
}
