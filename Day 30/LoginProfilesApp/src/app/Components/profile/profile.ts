import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { authService } from '../../Services/auth.service';
import { userModel } from '../../Models/user.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.html'
})
export class Profile {
  user: userModel | null = null;

  constructor(private _authService: authService) {
    this.user = this._authService.getLoggedInUser();
  }

  logout() {
    sessionStorage.removeItem('loggedInUser');
    this.user = null;
  }
}
