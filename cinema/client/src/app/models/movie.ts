import { Time } from "@angular/common";
import { GenreType } from "../enums/genreType";
import { Showing } from "./showing";

export interface Movie {
    id: string;
    title: string;
    description: string;
    duration: string;
    photoUrl: string;
    genres: { "name": GenreType }[];
    showings: Showing[];
}