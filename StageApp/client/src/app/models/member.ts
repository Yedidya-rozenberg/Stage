import { Photo } from "./photo";

export interface Member {
    id:           number;
    userName:     string;
    photo:        Photo;
    age:          number;
    knownAs:      string;
    phoneNumber:  string;
    email:        string;
    created:      Date;
    lastActive:   Date;
}

