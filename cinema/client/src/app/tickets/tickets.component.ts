import { Component, OnInit } from '@angular/core';
import { Ticket } from '../models/ticket';
import { TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {

  public tickets: Ticket[] | undefined;

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        this.tickets = tickets;
        console.log(tickets);
      }
    })
  };
}
