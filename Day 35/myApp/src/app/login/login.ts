import { Component } from '@angular/core';
import { UserLoginModel } from '../models/userLoginModel';
import { userService } from '../services/userService';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { textValidator } from '../misc/TextValidator';

@Component({
  selector: 'app-login',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user:UserLoginModel = new UserLoginModel();
  loginForm : FormGroup;

  constructor(private _userService:userService, private route:Router){
    this.loginForm = new FormGroup({
    un:new FormControl(null,Validators.required),
    pass:new FormControl(null,[Validators.required,textValidator()])
  })
  }

  public get un() : any {
    return this.loginForm.get("un")
  }

  public get pass() : any {
    return this.loginForm.get("pass")
  }

  handleLogin(){
    if(this.loginForm.invalid)
      return;
    this._userService.validateUserLogin(this.user);
    this.route.navigateByUrl("/home/"+this.user.username);
  }

  // handleLogin(un:any, pass:any){
  //   if(un.control.errors || pass.control.errors)
  //   {
  //     return;
  //   }
  //   this._userService.validateUserLogin(this.user);
  //   this.route.navigateByUrl("/home/"+this.user.username);
  // }
}
