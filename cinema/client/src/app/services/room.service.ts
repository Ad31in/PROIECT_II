import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Movie } from '../models/movie';
import { Room } from '../models/room';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  baseUrl = environment.apiUrl;
  rooms: Room[] = [];
  room: Room | undefined;
  constructor(private http: HttpClient) { }

  getRooms() {
    return this.http.get<Room[]>(this.baseUrl + "room").pipe(
      map((rooms) => {
        this.rooms = rooms;
        return rooms;
      })
    )
  }

  addRoom(model: any): Observable<Room> {
    return this.http.post<Room>(this.baseUrl + "room", model).pipe(
      map((room) => room)
    );
  }

  getRoomById(id: string): Observable<Room> {
    return this.http.get<Room>(this.baseUrl + "room/" + id).pipe(
      map((room) => {
        return room;
      })
    )
  }

  getRoomWithSeatStateById(roomId: string, showId: string): Observable<Room> {
    return this.http.get<Room>(this.baseUrl + "room/" + roomId + "/" + showId).pipe(
      map((room) => {
        return room;
      })
    )
  }
}
