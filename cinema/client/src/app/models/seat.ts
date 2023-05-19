import { SeatState } from "../enums/seatState";

export interface Seat {
    id: string;
    roomId: string;
    row: number;
    number: number;
    state: SeatState
}