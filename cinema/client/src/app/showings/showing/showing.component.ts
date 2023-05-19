import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ActionType } from 'src/app/enums/actionType';
import { GetTicket } from 'src/app/models/getTicket';
import { Seat } from 'src/app/models/seat';
import { Showing } from 'src/app/models/showing';
import { ShowingService } from 'src/app/services/showing.service';
import { TicketService } from 'src/app/services/ticket.service';

@Component({
  selector: 'app-showing',
  templateUrl: './showing.component.html',
  styleUrls: ['./showing.component.scss']
})
export class ShowingComponent implements OnInit {
  public showingId = '';
  public actionType: ActionType | undefined;
  public isSelected = false;
  public showing: Showing | undefined;
  public selectedSeat: Seat | undefined;

  constructor(
    private showingService: ShowingService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private ticketService: TicketService,
    private router: Router
  ) { }

  ngOnInit() {
    this.showingId = this.route.snapshot.paramMap.get('showingId') || '';
    console.log(this.showingId);
    if (this.showingId) {
      this.showingService.getShowingById(this.showingId).subscribe({
        next: (showing) => {
          this.showing = showing;
          console.log(showing);
        }
      });
    }
  }

  onSeatSelected(seat: Seat) {
    this.selectedSeat = seat;
    console.log('Selected seat:', seat);
  }

  selectSeat(seat?: Seat) {
    if (seat) {
      console.log(seat);
      this.isSelected = true;
    } else {
      this.toastr.error('Select a seat');
    }
  }

  back() {
    this.isSelected = false;
  }

  getTicket(actionType: ActionType) {
    const model: GetTicket = {
      seatId: '',
      showingId: ''
    };

    if (actionType === ActionType.buy && this.selectedSeat) {
      model.seatId = this.selectedSeat.id;
      model.showingId = this.showingId;
      this.ticketService.buyTicket(model).subscribe({
        next: () => {
          this.toastr.success('Ticket was bought successfully');
          this.router.navigateByUrl('/tickets');
        }
      })
      console.log('Buy', model);
    } else if (this.selectedSeat) {
      model.seatId = this.selectedSeat.id;
      model.showingId = this.showingId;
      this.toastr.success('Ticket was reserved successfully');
      this.router.navigateByUrl('/tickets');
      // this.ticketService.reserveTicket(model).subscribe({
      //   next: () => {
      //     this.toastr.success('Ticket was bought successfully');
      //     this.router.navigateByUrl('');
      //   }
      // })
    } else {
      this.toastr.error('Select a seat');
    }
  }
}