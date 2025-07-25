import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StaffRightSidebarComponent } from './right-sidebar';
import { StaffService } from '../../services/staff.service';
import { NotificationService } from '../../services/notification.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('StaffRightSidebarComponent', () => {
  let component: StaffRightSidebarComponent;
  let fixture: ComponentFixture<StaffRightSidebarComponent>;
  let staffServiceMock: any;
  let notificationServiceMock: any;
  let authMock: any;
  let routerMock: any;

  beforeEach(() => {
    staffServiceMock = {
      getUserProfile: jasmine.createSpy('getUserProfile').and.returnValue(of({ username: 'staff' }))
    };
    notificationServiceMock = {
      unreadCount$: of(2),
      notifications$: of([]),
      markAllAsRead: jasmine.createSpy('markAllAsRead'),
      clearAllNotifications: jasmine.createSpy('clearAllNotifications')
    };
    authMock = {
      logout: jasmine.createSpy('logout')
    };
    routerMock = {
      navigate: jasmine.createSpy('navigate')
    };

    TestBed.configureTestingModule({
      imports: [StaffRightSidebarComponent],
      providers: [
        { provide: StaffService, useValue: staffServiceMock },
        { provide: NotificationService, useValue: notificationServiceMock },
        { provide: AuthService, useValue: authMock },
        { provide: Router, useValue: routerMock }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(StaffRightSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component and load user profile', () => {
    expect(component).toBeTruthy();
    expect(staffServiceMock.getUserProfile).toHaveBeenCalled();
  });

  it('should toggle notifications and mark as read', () => {
    component.toggleNotifications();
    expect(component.showNotifications).toBeTrue();
    expect(notificationServiceMock.markAllAsRead).toHaveBeenCalled();
  });

  it('should logout on confirmation', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.logout();
    expect(authMock.logout).toHaveBeenCalled();
    expect(routerMock.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should clear notifications on confirmation', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.clearNotifications();
    expect(notificationServiceMock.clearAllNotifications).toHaveBeenCalled();
  });
});
