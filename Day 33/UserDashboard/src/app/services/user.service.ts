import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "../models/user.model";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class UserService 
{
    private apiUrl = 'https://dummyjson.com/users';

    constructor(private http: HttpClient)
    {

    }

    getUsers() : Observable<User[]>
    {
        return this.http.get<User[]>(`${this.apiUrl}`);
    }

    addUser(user: Partial<User>) : Observable<any>
    {
        return this.http.post(`${this.apiUrl}/add`,user);
    }
}