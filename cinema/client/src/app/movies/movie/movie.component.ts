import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/models/movie';
import { MovieService } from 'src/app/services/movie.service';
import { Pipe, PipeTransform } from '@angular/core';
import { GenreType } from 'src/app/enums/genreType';

@Pipe({
  name: 'genreType'
})
export class GenreTypePipe implements PipeTransform {

  transform(value: GenreType): string {
    switch (value) {
      case GenreType.Action:
        return 'Action';
      case GenreType.Romance:
        return 'Romance';
      case GenreType.Horror:
        return 'Horror';
      case GenreType.Comedy:
        return 'Comedy';
      case GenreType.Adventure:
        return 'Adventure';
      case GenreType.SF:
        return 'SF';
      default:
        return '';
    }
  }
}


@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {
  public movie: Movie | undefined;

  constructor(private movieService: MovieService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('movieId');

    if (id != null) {
      this.movieService.getMovieById(id).subscribe({
        next: (movie) => {
          this.movie = movie;
          console.log(movie.genres[0].name);
        }
      })
    }
  };

  goToShowing(id: string) {
    this.router.navigateByUrl(`showings/${id}`)
  }
}

