import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AddItemComponent } from './add-item';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('AddItemComponent', () => {
  let component: AddItemComponent;
  let fixture: ComponentFixture<AddItemComponent>;
  let adminServiceSpy: jasmine.SpyObj<AdminService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('AdminService', ['createItem']);

    await TestBed.configureTestingModule({
      imports: [AddItemComponent,ReactiveFormsModule, FormsModule],
      providers: [{ provide: AdminService, useValue: spy }]
    }).compileComponents();

    fixture = TestBed.createComponent(AddItemComponent);
    component = fixture.componentInstance;
    adminServiceSpy = TestBed.inject(AdminService) as jasmine.SpyObj<AdminService>;

    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with empty values', () => {
    expect(component.itemForm.value).toEqual({
      name: '',
      description: '',
      categories: ''
    });
  });

  it('should disable submit if form is invalid', () => {
    component.submit();
    expect(adminServiceSpy.createItem).not.toHaveBeenCalled();
  });

  it('should call createItem and emit itemCreated on successful submit', fakeAsync(() => {
    const emitSpy = spyOn(component.itemCreated, 'emit');
    const alertSpy = spyOn(window, 'alert');

    component.itemForm.setValue({
      name: 'Item 1',
      description: 'Test item',
      categories: 'Category A'
    });

    const expectedPayload = {
      name: 'Item 1',
      description: 'Test item',
      categories: ['Category A']
    };

    adminServiceSpy.createItem.and.returnValue(of({}));
    component.submit();
    tick();

    expect(adminServiceSpy.createItem).toHaveBeenCalledWith(expectedPayload);
    expect(alertSpy).toHaveBeenCalledWith('Item added successfully!');
    expect(emitSpy).toHaveBeenCalled();
  }));

  it('should alert and log error if createItem fails', fakeAsync(() => {
    const alertSpy = spyOn(window, 'alert');
    const consoleSpy = spyOn(console, 'error');

    component.itemForm.setValue({
      name: 'Invalid',
      description: 'Error case',
      categories: 'Cat B'
    });

    const errorResponse = {
      error: { title: 'Item creation failed' }
    };

    adminServiceSpy.createItem.and.returnValue(throwError(() => errorResponse));

    component.submit();
    tick();

    expect(consoleSpy).toHaveBeenCalledWith('Failed to add item:', errorResponse);
    expect(alertSpy).toHaveBeenCalledWith('Failed to add item:\nItem creation failed');
  }));

  it('should emit cancel on cancelForm confirmation', () => {
    const emitSpy = spyOn(component.cancel, 'emit');
    spyOn(window, 'confirm').and.returnValue(true);

    component.cancelForm();

    expect(emitSpy).toHaveBeenCalled();
  });

  it('should not emit cancel if user declines confirmation', () => {
    const emitSpy = spyOn(component.cancel, 'emit');
    spyOn(window, 'confirm').and.returnValue(false);

    component.cancelForm();

    expect(emitSpy).not.toHaveBeenCalled();
  });

  it('should mark name as invalid if less than 3 chars', () => {
    const nameControl = component.name;
    nameControl?.setValue('Hi');
    expect(nameControl?.valid).toBeFalse();
    expect(nameControl?.errors?.['minlength']).toBeTruthy();
  });

  it('should mark all fields as valid if correct values provided', () => {
    component.itemForm.setValue({
      name: 'Valid Name',
      description: 'Some desc',
      categories: 'Category X'
    });
    expect(component.itemForm.valid).toBeTrue();
  });
});