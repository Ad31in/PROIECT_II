import { Component, OnInit } from '@angular/core';
import { ShowingService } from '../services/showing.service';
import { RoomService } from '../services/room.service';
import { MovieService } from '../services/movie.service';
import { Movie } from '../models/movie';
import { Room } from '../models/room';
import { Showing } from '../models/showing';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-showing',
  templateUrl: './add-showing.component.html',
  styleUrls: ['./add-showing.component.scss'],
})
export class AddShowingComponent implements OnInit {
  movies: Movie[] = [];
  rooms: Room[] = [];
  selectedMovie: Movie | undefined;
  startDate: string = '';
  availableRooms: Room[] = [];
  selectedTime: Date = new Date();
  selectedRoom: any = {};
  model: any = {};

  constructor(
    private showingService: ShowingService,
    private roomService: RoomService,
    private movieService: MovieService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeMovies();
    this.initializeRooms();
  }

  initializeMovies() {
    this.movieService.getMovies().subscribe((response) => {
      this.movies = response;
    });
  }

  initializeRooms() {
    this.roomService.getRooms().subscribe((response) => {
      this.rooms = response;
      console.log(this.rooms);
    });
  }

  onMovieSelected() {
    if (this.selectedTime != null && this.selectedMovie) {
      console.log(this.selectedTime, this.selectedMovie);
      this.model.startDate = this.selectedTime;
      const duration = this.selectedMovie.duration;
      const selectedDuration = this.parseDuration(duration);
      const date = this.selectedTime;
      const selectedDate = new Date(date);
      const selectedDateOnly = new Date(
        selectedDate.getFullYear(),
        selectedDate.getMonth(),
        selectedDate.getDate(),
        selectedDate.getHours(),
        selectedDate.getMinutes()
      );
      this.availableRooms = this.rooms.filter((room: Room) => {
        if (!room.showings || room.showings.length == 0) {
          return true;
        }

        const showings = room.showings.filter((showing: Showing) => {
          const showingStartDate = new Date(showing.startDate);
          const showingEndDate = new Date(showing.endDate);
          console.log(showingEndDate);

          // Check if the selected date falls outside the showing's start and end dates
          const isBeforeShowing = selectedDateOnly < showingStartDate;
          const isAfterShowing = selectedDateOnly > showingEndDate;

          // Check if the showing duration is greater than or equal to the selected duration
          // const hasEnoughTime = showingEndDate - showingStartDate >= selectedDuration;

          return isBeforeShowing || isAfterShowing; //|| !hasEnoughTime;
        });

        return showings.length === room.showings.length;
      });
    }
  }

  parseDuration(duration: string): number {
    // Assuming the Time format is 'HH:MM:SS'
    const [hours, minutes, seconds] = duration.split(':').map(Number);
    const durationInMilliseconds =
      hours * 60 * 60 * 1000 + minutes * 60 * 1000 + seconds * 1000;
    return durationInMilliseconds;
  }

  addShowing(): void {
    this.model.startDate = new Date(this.selectedTime);
    this.model.roomId = this.selectedRoom.id;
    this.model.movieId = this.selectedMovie?.id;
    console.log(this.model);

    this.showingService.addShowing(this.model).subscribe(
      () => {
        this.toastr.success('Shwing was added successfully!');
        this.initializeRooms();
        this.router.navigateByUrl('/');
      },
      () => {
        this.toastr.error('There was a problem!');
      }
    );
    // You can access the selected movie, room, and date using the respective properties in this component
  }
}
