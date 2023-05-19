import { Movie } from "./movie";
import { Room } from "./room";

export interface Showing {
    id: string;
    startDate: Date;
    endDate: Date;
    movie?: Movie;
    room?: Room;
    price: number;
}