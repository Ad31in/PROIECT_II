import { Component } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { ToastrService } from 'ngx-toastr';
import { Movie } from '../models/movie';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.scss']
})
export class AddMovieComponent {
  model: any = {};
  genres = [{ id: "Action", name: 0 },
  { id: "Romance", name: 1 },
  { id: "Horror", name: 2 },
  { id: "Comedy", name: 3 },
  { id: "Adventure", name: 4 },
  { id: "SF", name: 5 }];
  public movieAdded = false;
  public movie: Movie | undefined;
  public selectedPhoto: File | null = null;

  constructor(private router: Router, private movieService: MovieService, private toastr: ToastrService) { }

  addMovie() {
    this.movieService.addMovie(this.model).subscribe(
      (movie) => {
        this.toastr.success('Movie added successfully');
        this.movie = movie;
      },
      (error) => {
        this.toastr.error('Error adding movie: ' + error.message);
      }
    );
  }

  uploadPhoto() {
    if (this.movie && this.selectedPhoto != null) {
      this.movieService.uploadPhoto(this.movie.id, this.selectedPhoto).subscribe(
        (response) => {
          this.toastr.success('Photo added successfully');
          this.router.navigateByUrl('');
        },
        (error) => {
          this.toastr.error('Failed to add photo');
        }
      )
    }

  }

  onFileSelected(input: EventTarget | null) {
    const fileInput = input as HTMLInputElement;
    if (fileInput && fileInput.files && fileInput.files.length > 0) {
      this.selectedPhoto = fileInput.files[0];
    } else {
      this.selectedPhoto = null;
    }
  }
}