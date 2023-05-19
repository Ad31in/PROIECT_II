import { Component } from '@angular/core';
import { RoomService } from '../services/room.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent {
  model: any = {
    number: null,
    capacity: null
  };

  constructor(private roomService: RoomService, private toastr: ToastrService) { }

  addRoom() {
    if (this.model.capacity < 10) {
      this.toastr.error("Add more seats");
      return;
    }
    this.roomService.addRoom(this.model).subscribe(
      () => {
        this.toastr.success("Room added successfully");
        this.model = {
          number: null,
          capacity: null
        }
      },
      () => {
        this.toastr.error("This room already exists");
      }
    );
  }
}
