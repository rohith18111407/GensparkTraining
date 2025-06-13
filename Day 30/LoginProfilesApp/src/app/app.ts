import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Login } from './Components/login/login';
import { Profile } from './Components/profile/profile';
import { authService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, Login, Profile],
  templateUrl: './app.html'
})
export class App {
  isLoggedIn = false;

  constructor(private _authService: authService) {
    this.isLoggedIn = !!this._authService.getLoggedInUser();
  }

  onLogin() {
    this.isLoggedIn = true;
  }

  onLogout() {
    this.isLoggedIn = false;
  }
}