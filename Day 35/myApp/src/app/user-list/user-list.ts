import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { Store } from '@ngrx/store';
import { selectAllUsers, selectUserError, selectUserLoading } from '../ngrx/user.selector';
import { AddUser } from "../add-user/add-user";
import { AsyncPipe, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-user-list',
  imports: [AddUser,NgIf,NgFor,AsyncPipe],
  templateUrl: './user-list.html',
  styleUrl: './user-list.css'
})
export class UserList implements OnInit{

  users$ : Observable<User[]>;
  loading$ : Observable<boolean>;
  error$ : Observable<string | null>;

  constructor(private store: Store)
  {
    this.users$ = this.store.select(selectAllUsers);
    this.loading$ = this.store.select(selectUserLoading);
    this.error$ = this.store.select(selectUserError);
  }

  ngOnInit(): void {
    this.store.dispatch({type : '[Users] Load Users'});
  }

}
