import { Injectable } from '@angular/core';
import { Ticket } from '../models/ticket';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  baseUrl = environment.apiUrl;
  showing: Ticket | undefined;
  constructor(private http: HttpClient) { }


  buyTicket(model: any): Observable<Ticket> {
    return this.http.post<Ticket>(this.baseUrl + "ticket/buy", model).pipe(
      map((showing) => showing)
    );
  }

  reserveTicket(model: any): Observable<Ticket> {
    return this.http.post<Ticket>(this.baseUrl + "ticket/reserve", model).pipe(
      map((showing) => showing)
    );
  }

  getTicketById(id: string): Observable<Ticket> {
    return this.http.get<Ticket>(this.baseUrl + "ticket/" + id).pipe(
      map((ticket) => {
        return ticket;
      })
    );
  }

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl + "ticket").pipe(
      map((tickets) => {
        return tickets;
      })
    );
  }
}
