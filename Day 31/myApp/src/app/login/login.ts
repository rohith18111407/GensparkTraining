import { Component } from '@angular/core';
import { UserLoginModel } from '../models/userLoginModel';
import { userService } from '../services/userService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user:UserLoginModel = new UserLoginModel();
  constructor(private _userService:userService){

  }
  handleLogin(){
    this._userService.validateUserLogin(this.user);
  }
}
