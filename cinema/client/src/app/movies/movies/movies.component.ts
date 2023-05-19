import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/models/movie';
import { AccountService } from 'src/app/services/account.service';
import { MovieService } from 'src/app/services/movie.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {
  public movies$: Observable<Movie[]> | undefined;

  constructor(private movieService: MovieService, private router: Router, public accountService: AccountService) {

  }

  ngOnInit(): void {
    this.movies$ = this.movieService.getMovies();
  }

  goToShowing(id: string) {
    this.router.navigateByUrl(`showings/${id}`)
  }
}
