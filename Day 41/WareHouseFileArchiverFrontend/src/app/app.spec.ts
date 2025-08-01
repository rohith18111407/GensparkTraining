import { ComponentFixture, TestBed } from '@angular/core/testing';
import { App } from './app';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

describe('App Component', () => {
  let component: App;
  let fixture: ComponentFixture<App>;
  let mockAuthService: any;
  let router: Router;

  beforeEach(async () => {
    mockAuthService = {
      isLoggedIn: jasmine.createSpy('isLoggedIn'),
      setupTokenMonitor: jasmine.createSpy('setupTokenMonitor'),
      getAccessToken: jasmine.createSpy('getAccessToken'),
      logout: jasmine.createSpy('logout')
    };

    await TestBed.configureTestingModule({
      imports: [App, RouterTestingModule],
      providers: [
        { provide: AuthService, useValue: mockAuthService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(App);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
  });

  it('should create the App component', () => {
    expect(component).toBeTruthy();
  });

  it('should call setupTokenMonitor if logged in on init', () => {
    mockAuthService.isLoggedIn.and.returnValue(true);
    component.ngOnInit();
    expect(mockAuthService.setupTokenMonitor).toHaveBeenCalled();
  });

  it('should NOT call setupTokenMonitor if not logged in on init', () => {
    mockAuthService.isLoggedIn.and.returnValue(false);
    component.ngOnInit();
    expect(mockAuthService.setupTokenMonitor).not.toHaveBeenCalled();
  });

  it('should return true from isLoggedIn if access token exists', () => {
    mockAuthService.getAccessToken.and.returnValue('abc123');
    expect(component.isLoggedIn()).toBeTrue();
  });

  it('should return false from isLoggedIn if no access token', () => {
    mockAuthService.getAccessToken.and.returnValue(null);
    expect(component.isLoggedIn()).toBeFalse();
  });

  it('should call logout and navigate to login', () => {
    spyOn(router, 'navigate');
    component.logout();
    expect(mockAuthService.logout).toHaveBeenCalled();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });
});