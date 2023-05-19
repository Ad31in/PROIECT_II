import { Seat } from "./seat";
import { Showing } from "./showing";

export interface Room {
    id: string;
    number: number;
    capacity: number;
    seats?: Seat[];
    showings?: Showing[];
}