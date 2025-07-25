import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
 import { AuthService } from '../services/auth.service';
import { LoginResponse } from '../models/auth.model';
import { CommonModule } from '@angular/common';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login';
 
// Mock AuthService
class MockAuthService {
  login() {
    // Default mock, can be overridden with spyOn
    return of({});
  }
  storeTokens() {
    // Mocked implementation
  }
  getUserRole() {
    // Mocked implementation
    return 'Admin'; // Default role for testing
  }
}
 
describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let router: Router;
 
  // Mock response data for successful login
  const mockLoginResponse: LoginResponse = {
    success: true,
    message: 'Login successful',
    data: {
      jwtToken: 'fake-jwt-token',
      refreshToken: 'fake-refresh-token',
      role: 'Admin',
      jwtExpiryTime: new Date(Date.now() + 3600 * 1000).toISOString(),
    },
    errors: null,
  };
 
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // Since the component is standalone, we import it directly.
      imports: [
        LoginComponent,
        ReactiveFormsModule,
        CommonModule,
        RouterTestingModule.withRoutes([]), // Use RouterTestingModule for routing dependencies
      ],
      providers: [
        // Provide the mock service for AuthService
        { provide: AuthService, useClass: MockAuthService },
      ],
    }).compileComponents();
 
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService); // Inject the service
    router = TestBed.inject(Router); // Inject the router
 
    fixture.detectChanges(); // Initial data binding
  });
 
  it('should create the component', () => {
    expect(component).toBeTruthy();
  });
 
  it('should initialize the login form with empty username and password', () => {
    expect(component.loginForm).toBeDefined();
    expect(component.loginForm.get('username')).toBeDefined();
    expect(component.loginForm.get('password')).toBeDefined();
    expect(component.loginForm.value).toEqual({ username: '', password: '' });
  });
 
  it('should make the form invalid when fields are empty', () => {
    expect(component.loginForm.valid).toBeFalsy();
  });
 
  it('should make the username control required', () => {
    const usernameControl = component.loginForm.get('username');
    usernameControl?.setValue('');
    expect(usernameControl?.hasError('required')).toBeTruthy();
  });
 
  it('should make the password control required', () => {
    const passwordControl = component.loginForm.get('password');
    passwordControl?.setValue('');
    expect(passwordControl?.hasError('required')).toBeTruthy();
  });
 
  it('should make the form valid when both fields are filled', () => {
    component.loginForm.setValue({ username: 'testuser', password: 'password123' });
    expect(component.loginForm.valid).toBeTruthy();
  });
 
  describe('onSubmit', () => {
    beforeEach(() => {
        // Set form values before each test in this block
        component.loginForm.setValue({ username: 'testuser', password: 'password123' });
    });
 
    it('should not call auth.login if the form is invalid', () => {
      component.loginForm.reset(); // Make the form invalid
      const loginSpy = spyOn(authService, 'login').and.callThrough();
      component.onSubmit();
      expect(loginSpy).not.toHaveBeenCalled();
    });
 
    it('should call auth.login and navigate to admin dashboard on successful login as Admin', () => {
      // Spy on service and router methods
      const loginSpy = spyOn(authService, 'login').and.returnValue(of(mockLoginResponse));
      const storeTokensSpy = spyOn(authService, 'storeTokens').and.callThrough();
      const getUserRoleSpy = spyOn(authService, 'getUserRole').and.returnValue('Admin');
      const navigateSpy = spyOn(router, 'navigate');
 
      component.onSubmit();
 
      // Expectations
      expect(loginSpy).toHaveBeenCalledWith({ username: 'testuser', password: 'password123' });
      expect(storeTokensSpy).toHaveBeenCalledWith(
        mockLoginResponse.data.jwtToken,
        mockLoginResponse.data.refreshToken,
        mockLoginResponse.data.role,
        mockLoginResponse.data.jwtExpiryTime
      );
      expect(getUserRoleSpy).toHaveBeenCalled();
      expect(navigateSpy).toHaveBeenCalledWith(['/admin/dashboard']);
      expect(component.errorMessage).toBe('');
    });
 
    it('should call auth.login and navigate to staff dashboard on successful login as Staff', () => {
        const staffResponse = {
            ...mockLoginResponse,
            data: { ...mockLoginResponse.data, role: 'Staff' }
        };
        spyOn(authService, 'login').and.returnValue(of(staffResponse));
        spyOn(authService, 'storeTokens');
        spyOn(authService, 'getUserRole').and.returnValue('Staff');
        const navigateSpy = spyOn(router, 'navigate');
 
        component.onSubmit();
 
        expect(navigateSpy).toHaveBeenCalledWith(['/staff/dashboard']);
    });
 
    it('should set an error message for an unauthorized role', () => {
        const unauthorizedResponse = {
            ...mockLoginResponse,
            data: { ...mockLoginResponse.data, role: 'Guest' }
        };
        spyOn(authService, 'login').and.returnValue(of(unauthorizedResponse));
        spyOn(authService, 'storeTokens');
        spyOn(authService, 'getUserRole').and.returnValue('Guest');
        const navigateSpy = spyOn(router, 'navigate');
 
        component.onSubmit();
 
        expect(navigateSpy).not.toHaveBeenCalled();
        expect(component.errorMessage).toBe('Unauthorized role');
    });
 
    it('should set an error message on login failure', () => {
      const errorResponse = { status: 401, error: { message: 'Invalid credentials' } };
      spyOn(authService, 'login').and.returnValue(throwError(() => errorResponse));
      const navigateSpy = spyOn(router, 'navigate');
 
      component.onSubmit();
 
      expect(navigateSpy).not.toHaveBeenCalled();
      expect(component.errorMessage).toBe('Invalid username or password.');
    });
  });
 
  it('should return the username form control from the getter', () => {
    expect(component.username).toBe(component.loginForm.get('username'));
  });
 
});