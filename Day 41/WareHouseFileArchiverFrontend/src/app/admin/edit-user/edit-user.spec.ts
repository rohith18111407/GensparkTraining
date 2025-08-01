import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditUserComponent } from './edit-user';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('EditUserComponent', () => {
  let component: EditUserComponent;
  let fixture: ComponentFixture<EditUserComponent>;
  let adminServiceSpy: jasmine.SpyObj<AdminService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('AdminService', ['updateUser']);

    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, EditUserComponent],
      providers: [{ provide: AdminService, useValue: spy }]
    }).compileComponents();

    fixture = TestBed.createComponent(EditUserComponent);
    component = fixture.componentInstance;
    adminServiceSpy = TestBed.inject(AdminService) as jasmine.SpyObj<AdminService>;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should patch form values on ngOnInit if user input is provided', () => {
    component.user = { id: '1', username: 'test@example.com', roles: ['Admin'] };
    component.ngOnInit();
    expect(component.form.value).toEqual({
      username: 'test@example.com',
      roles: ['Admin']
    });
  });

  it('should disable submit button if form is invalid', () => {
    fixture.detectChanges();
    const button = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
    expect(button.disabled).toBeTrue();
  });

  it('should emit cancel when Cancel button is clicked', () => {
    spyOn(component.cancel, 'emit');
    fixture.detectChanges();
    const cancelBtn = fixture.debugElement.query(By.css('button.btn-secondary'));
    cancelBtn.nativeElement.click();
    expect(component.cancel.emit).toHaveBeenCalled();
  });

  it('should call updateUser and emit userUpdated on successful submit', () => {
    spyOn(window, 'alert');
    spyOn(component.userUpdated, 'emit');

    const mockUser = { id: '1', username: 'admin@example.com', roles: ['Admin'] };
    component.user = mockUser;
    component.ngOnInit();

    component.form.setValue({
      username: mockUser.username,
      roles: mockUser.roles
    });

    adminServiceSpy.updateUser.and.returnValue(of({}));

    component.onSubmit();

    expect(adminServiceSpy.updateUser).toHaveBeenCalledWith('1', {
      username: mockUser.username,
      roles: mockUser.roles
    });

    expect(window.alert).toHaveBeenCalledWith('User updated successfully!');
    expect(component.userUpdated.emit).toHaveBeenCalled();
  });

  it('should show alert on update failure', () => {
    spyOn(window, 'alert');

    const mockUser = { id: '1', username: 'fail@example.com', roles: ['Admin'] };
    component.user = mockUser;
    component.ngOnInit();

    component.form.setValue({
      username: mockUser.username,
      roles: mockUser.roles
    });

    adminServiceSpy.updateUser.and.returnValue(throwError(() => new Error('Error')));

    component.onSubmit();

    expect(window.alert).toHaveBeenCalledWith('Failed to update user.');
  });

  it('should not submit if form is invalid or user id missing', () => {
    component.user = { username: '', roles: [] }; // No id
    component.form.setValue({ username: '', roles: [] });
    component.onSubmit();
    expect(adminServiceSpy.updateUser).not.toHaveBeenCalled();
  });
});