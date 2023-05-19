import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotfoundComponent } from './notfound/notfound/notfound.component';
import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';
import { MoviesComponent } from './movies/movies/movies.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { AddShowingComponent } from './add-showing/add-showing.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketComponent } from './ticket/ticket.component';
import { MovieComponent } from './movies/movie/movie.component';
import { ShowingComponent } from './showings/showing/showing.component';
import { ServerErrorComponent } from './server-error/server-error/server-error.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: '', component: MoviesComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'add-movie', component: AddMovieComponent, canActivate: [AdminGuard] },
      { path: 'add-showing', component: AddShowingComponent, canActivate: [AdminGuard] },
      { path: 'add-room', component: AddRoomComponent, canActivate: [AdminGuard] },
      { path: 'tickets', component: TicketsComponent },
      { path: ':movieId', component: MovieComponent },
      { path: 'tickets/:ticketId', component: TicketComponent },
      { path: 'showings/:showingId', component: ShowingComponent }
    ],
  },
  { path: 'not-found', component: NotfoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotfoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
