import { Component } from '@angular/core';
import { userService } from '../services/userService';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [RouterLink],
  templateUrl: './menu.html',
  styleUrl: './menu.css'
})
export class Menu {
  username$:any;
  usrname:string|null = "";

  constructor(private _userService:userService)
  {
    //this.username$ = this.userService.username$;
    this._userService.username$.subscribe(
      {
       next:(value) =>{
          this.usrname = value ;
        },
        error:(err)=>{
          alert(err);
        }
      }
    )
  }
}
