import { Component, EventEmitter, NgModule, Output } from '@angular/core';
import { authService } from '../../Services/auth.service';
import { Route, Router } from '@angular/router';
import { FormsModule, NgModel } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule,CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  username = '';
  password = '';
  error = '';

  @Output() loggedIn = new EventEmitter<void>();

  constructor(private _authService: authService) {}

  login() {
    const user = this._authService.login(this.username, this.password);
    if (user) {
      this.loggedIn.emit();
    } else {
      this.error = 'Invalid credentials';
    }
  }
}
