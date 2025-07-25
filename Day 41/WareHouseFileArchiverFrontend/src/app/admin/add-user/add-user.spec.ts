import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AddUserComponent } from './add-user';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('AddUserComponent', () => {
  let component: AddUserComponent;
  let fixture: ComponentFixture<AddUserComponent>;
  let mockAdminService: jasmine.SpyObj<AdminService>;

  beforeEach(() => {
    mockAdminService = jasmine.createSpyObj('AdminService', ['createUser']);

    TestBed.configureTestingModule({
      imports: [AddUserComponent],
      providers: [{ provide: AdminService, useValue: mockAdminService }]
    });

    fixture = TestBed.createComponent(AddUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should have invalid form when fields are empty', () => {
    expect(component.userForm.valid).toBeFalse();
  });

  it('should validate email format', () => {
    component.userForm.patchValue({
      username: 'invalid-email',
      password: '123456',
      role: 'Admin'
    });
    expect(component.username?.valid).toBeFalse();
  });

  it('should validate password length', () => {
    component.userForm.patchValue({
      username: 'test@example.com',
      password: '123',
      role: 'Admin'
    });
    expect(component.password?.valid).toBeFalse();
  });

  it('should call adminService.createUser and emit on success', fakeAsync(() => {
    spyOn(window, 'alert');
    spyOn(component.userCreated, 'emit');
    const payload = {
      username: 'test@example.com',
      password: 'password123',
      roles: ['Staff']
    };

    mockAdminService.createUser.and.returnValue(of({}));

    component.userForm.setValue({
      username: payload.username,
      password: payload.password,
      role: 'Staff'
    });

    component.submit();
    tick();

    expect(mockAdminService.createUser).toHaveBeenCalledWith(payload);
    expect(component.userCreated.emit).toHaveBeenCalled();
    expect(window.alert).toHaveBeenCalledWith('User added successfully!');
  }));

  it('should alert on error during submission', fakeAsync(() => {
    spyOn(window, 'alert');
    mockAdminService.createUser.and.returnValue(throwError(() => new Error('Error')));

    component.userForm.setValue({
      username: 'test@example.com',
      password: 'password123',
      role: 'Admin'
    });

    component.submit();
    tick();

    expect(window.alert).toHaveBeenCalledWith('Failed to add user.');
  }));

  it('should emit cancel on cancelForm if confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    spyOn(component.cancel, 'emit');
    component.cancelForm();
    expect(component.cancel.emit).toHaveBeenCalled();
  });

  it('should not emit cancel if user declines confirmation', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    spyOn(component.cancel, 'emit');
    component.cancelForm();
    expect(component.cancel.emit).not.toHaveBeenCalled();
  });
});
