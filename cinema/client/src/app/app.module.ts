import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotfoundComponent } from './notfound/notfound/notfound.component';
import { ServerErrorComponent } from './server-error/server-error/server-error.component';
import { MoviesComponent } from './movies/movies/movies.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { AddShowingComponent } from './add-showing/add-showing.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketComponent } from './ticket/ticket.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { RegisterComponent } from './register/register.component';
import { ToastrModule } from 'ngx-toastr';
import { GenreTypePipe, MovieComponent } from './movies/movie/movie.component';
import { ShowingComponent } from './showings/showing/showing.component';
import { RoomComponent } from './room/room.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './_forms/text-input/text-input.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    NotfoundComponent,
    ServerErrorComponent,
    MoviesComponent,
    AddMovieComponent,
    AddShowingComponent,
    TicketsComponent,
    TicketComponent,
    AddRoomComponent,
    RegisterComponent,
    MovieComponent,
    ShowingComponent,
    RoomComponent,
    GenreTypePipe,
    TextInputComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgSelectModule,
    NgxMatTimepickerModule,
    NgxMatDatetimePickerModule,
    ReactiveFormsModule,
    BsDropdownModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    NgbModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
