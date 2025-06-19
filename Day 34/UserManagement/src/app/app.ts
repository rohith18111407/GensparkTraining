import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserManagementComponent } from "./user-management/user-management";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UserManagementComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserManagement';
}
