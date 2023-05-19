import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Room } from '../models/room';
import { RoomService } from '../services/room.service';
import { ActivatedRoute } from '@angular/router';
import { Seat } from '../models/seat';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit {
  @Input() roomId: string | undefined;
  @Input() showId: string | undefined;
  @Output() seatSelected = new EventEmitter<Seat>();
  public selectedSeat: Seat | undefined;

  public room: Room | undefined;

  constructor(private roomService: RoomService) { }

  ngOnInit() {
    if (this.roomId != null && this.showId != null) {
      this.roomService.getRoomWithSeatStateById(this.roomId, this.showId).subscribe({
        next: (room) => {
          this.room = room;
          console.log(room);
        }
      })
    }
  };

  selectSeat(seat: Seat) {
    this.selectedSeat = seat;
    this.seatSelected.emit(seat);
  }
}
