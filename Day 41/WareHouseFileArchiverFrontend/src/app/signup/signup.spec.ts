import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SignupComponent } from './signup';
import { AuthService } from '../services/auth.service';
import { of, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

describe('SignupComponent', () => {
  let component: SignupComponent;
  let fixture: ComponentFixture<SignupComponent>;
  let mockAuthService: any;
  let mockRouter: any;

  beforeEach(async () => {
    mockAuthService = {
      register: jasmine.createSpy()
    };

    mockRouter = {
      navigate: jasmine.createSpy(),
      createUrlTree: jasmine.createSpy().and.returnValue({}),
      serializeUrl: jasmine.createSpy().and.returnValue('/login'),
      events: of({}) 
    };

    await TestBed.configureTestingModule({
      imports: [SignupComponent, FormsModule, ReactiveFormsModule, CommonModule],
      providers: [
        { provide: AuthService, useValue: mockAuthService },
        { provide: Router, useValue: mockRouter },
        { provide: ActivatedRoute, useValue: {} }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SignupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create the signup component', () => {
    expect(component).toBeTruthy();
  });

  it('should mark form invalid if username and password are empty', () => {
    component.signupForm.setValue({ username: '', password: '', roles: ['Staff'] });
    expect(component.signupForm.invalid).toBeTrue();
  });

  it('should not submit if form is invalid', () => {
    spyOn(component, 'onSubmit').and.callThrough();
    component.signupForm.setValue({ username: '', password: '', roles: ['Staff'] });

    component.onSubmit();

    expect(component.onSubmit).toHaveBeenCalled();
    expect(mockAuthService.register).not.toHaveBeenCalled();
  });

  it('should call register and navigate to /login on success', () => {
    const formData = { username: 'john', password: 'secret123', roles: ['Staff'] };
    mockAuthService.register.and.returnValue(of({}));

    component.signupForm.setValue(formData);
    component.onSubmit();

    expect(mockAuthService.register).toHaveBeenCalledWith(formData);
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should show error message on failed registration', () => {
    const formData = { username: 'john', password: 'secret123', roles: ['Staff'] };
    mockAuthService.register.and.returnValue(throwError(() => new Error('Failed')));

    component.signupForm.setValue(formData);
    component.onSubmit();

    expect(component.errorMessage).toBe('Registration failed. Only Staff allowed.');
  });

  it('should show error if password is less than 6 characters', () => {
    component.signupForm.setValue({ username: 'john', password: '123', roles: ['Staff'] });
    const passwordControl = component.signupForm.get('password');
    passwordControl?.markAsTouched();
    fixture.detectChanges();

    expect(passwordControl?.errors).toBeTruthy();
    expect(passwordControl?.errors?.['lenError']).toBeTrue();
  });

  it('should have default value of Staff role', () => {
    const rolesControl = component.signupForm.get('roles');
    expect(rolesControl?.value).toEqual(['Staff']);
  });
});
