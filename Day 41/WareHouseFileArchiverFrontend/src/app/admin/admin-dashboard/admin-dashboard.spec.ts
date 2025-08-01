import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';
import { AdminDashboardComponent } from './admin-dashboard';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../services/admin.service';
import { of } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';

// Mock child components
@Component({
  selector: 'app-left-sidebar',
  standalone: true,
  template: '<div>Mock Left Sidebar</div>',
})
class MockLeftSidebarComponent {}

@Component({
  selector: 'app-center-content',
  standalone: true,
  template: '<div>Mock Center Content</div>',
})
class MockCenterContentComponent {}

@Component({
  selector: 'app-right-sidebar',
  standalone: true,
  template: '<div>Mock Right Sidebar</div>',
})
class MockRightSidebarComponent {}

describe('AdminDashboardComponent', () => {
  let component: AdminDashboardComponent;
  let fixture: ComponentFixture<AdminDashboardComponent>;

  const adminServiceStub = {
    getDashboardData: () => of({}),
    getUserProfile: () => of({ id: 1, username: 'admin' }) // ✅ mock added
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        CommonModule,
        HttpClientModule,
        AdminDashboardComponent,
        MockLeftSidebarComponent,
        MockCenterContentComponent,
        MockRightSidebarComponent
      ],
      providers: [
        { provide: AdminService, useValue: adminServiceStub }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AdminDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});