import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { LeftSidebarComponent } from "../left-sidebar/left-sidebar";
import { CenterContentComponent } from "../center-content/center-content";
import { AdminService } from '../../services/admin.service';
import { RightSidebarCompponent } from '../right-sidebar/right-sidebar';

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, LeftSidebarComponent, CenterContentComponent, RightSidebarCompponent],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
export class AdminDashboardComponent {
  selectedView: 'dashboard' | 'files' | 'items' | 'users' | 'statistics' = 'dashboard';
  adminService = inject(AdminService);

  setView(view: 'dashboard' | 'files' | 'items' | 'users' | 'statistics') {
    this.selectedView = view;
  }
}
