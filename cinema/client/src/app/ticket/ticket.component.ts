import { Component, OnInit } from '@angular/core';
import { TicketService } from '../services/ticket.service';
import { ActivatedRoute } from '@angular/router';
import { Ticket } from '../models/ticket';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss']
})
export class TicketComponent implements OnInit {
  public ticketId: string = "";
  public ticket: Ticket | undefined;
  constructor(private ticketService: TicketService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.ticketId = this.route.snapshot.paramMap.get('ticketId') || '';
    console.log(this.ticketId);
    if (this.ticketId) {
      this.ticketService.getTicketById(this.ticketId).subscribe({
        next: (ticket) => {
          this.ticket = ticket;
          console.log(ticket);
        }
      });
    }
  }
}
