import { Component } from '@angular/core';
import { UserLoginModel } from '../models/userLoginModel';
import { userService } from '../services/userService';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user:UserLoginModel = new UserLoginModel();
  constructor(private _userService:userService, private route:Router){

  }
  handleLogin(){
    this._userService.validateUserLogin(this.user);
    this.route.navigateByUrl("/home/"+this.user.username);
  }
}
