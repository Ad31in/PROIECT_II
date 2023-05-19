import { Seat } from "./seat";
import { Showing } from "./showing";
import { User } from "./user";

export interface Ticket {
    id: string;
    showing: Showing;
    seat: Seat;
    user: User;
}