import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { RightSidebarCompponent } from './right-sidebar';
import { AdminService } from '../../services/admin.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../../services/notification.service';
import { of, throwError } from 'rxjs';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('RightSidebarCompponent', () => {
  let component: RightSidebarCompponent;
  let fixture: ComponentFixture<RightSidebarCompponent>;
  let adminServiceSpy: jasmine.SpyObj<AdminService>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let notificationServiceSpy: jasmine.SpyObj<NotificationService>;

  beforeEach(async () => {
    adminServiceSpy = jasmine.createSpyObj('AdminService', ['getUserProfile']);
    authServiceSpy = jasmine.createSpyObj('AuthService', ['logout']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    notificationServiceSpy = jasmine.createSpyObj('NotificationService', ['markAllAsRead', 'clearAllNotifications'], {
      unreadCount$: of(3),
      notifications$: of([]),
    });

    await TestBed.configureTestingModule({
      imports: [RightSidebarCompponent],
      providers: [
        { provide: AdminService, useValue: adminServiceSpy },
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: NotificationService, useValue: notificationServiceSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(RightSidebarCompponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load user on init', fakeAsync(() => {
    const mockUser = { userName: 'test', email: 'test@example.com', roles: ['Admin'] };
    adminServiceSpy.getUserProfile.and.returnValue(of(mockUser));
    component.ngOnInit();
    tick();
    expect(component.user).toEqual(mockUser);
    expect(component.loading).toBeFalse();
    expect(component.error).toBeNull();
  }));

  it('should handle user load error on init', fakeAsync(() => {
    adminServiceSpy.getUserProfile.and.returnValue(throwError(() => new Error('error')));
    component.ngOnInit();
    tick();
    expect(component.user).toBeNull();
    expect(component.loading).toBeFalse();
    expect(component.error).toBe('Failed to load user info');
  }));

  it('should toggle notifications and mark them as read', () => {
    component.showNotifications = false;
    component.toggleNotifications();
    expect(component.showNotifications).toBeTrue();
    expect(notificationServiceSpy.markAllAsRead).toHaveBeenCalled();
  });

  it('should logout if confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.logout();
    expect(authServiceSpy.logout).toHaveBeenCalled();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should not logout if not confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    component.logout();
    expect(authServiceSpy.logout).not.toHaveBeenCalled();
  });

  it('should clear notifications if confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.clearNotifications();
    expect(notificationServiceSpy.clearAllNotifications).toHaveBeenCalled();
  });

  it('should not clear notifications if not confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    component.clearNotifications();
    expect(notificationServiceSpy.clearAllNotifications).not.toHaveBeenCalled();
  });
});
