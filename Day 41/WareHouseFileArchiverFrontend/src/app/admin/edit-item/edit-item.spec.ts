import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { EditItemComponent } from './edit-item';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('EditItemComponent', () => {
  let component: EditItemComponent;
  let fixture: ComponentFixture<EditItemComponent>;
  let mockAdminService: jasmine.SpyObj<AdminService>;

  beforeEach(() => {
    mockAdminService = jasmine.createSpyObj('AdminService', ['updateItem']);

    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, EditItemComponent],
      providers: [{ provide: AdminService, useValue: mockAdminService }]
    });

    fixture = TestBed.createComponent(EditItemComponent);
    component = fixture.componentInstance;
  });

  it('should create component and form should be invalid initially', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
    expect(component.form.invalid).toBeTrue();
  });

  it('should populate form on ngOnInit with input item', () => {
    component.item = {
      id: '123',
      name: 'Test Item',
      description: 'Sample description',
      categories: ['Category1', 'Category2']
    };

    component.ngOnInit();

    expect(component.form.value.name).toBe('Test Item');
    expect(component.form.value.description).toBe('Sample description');
    expect(component.form.value.categories).toBe('Category1, Category2');
  });

  it('should emit cancel event on cancel button click', () => {
    spyOn(component.cancel, 'emit');
    fixture.detectChanges();

    const cancelButton = fixture.debugElement.query(By.css('button.btn-secondary'));
    cancelButton.triggerEventHandler('click', null);

    expect(component.cancel.emit).toHaveBeenCalled();
  });

  it('should not call updateItem if form is invalid or item is missing', () => {
    component.item = { id: '123' };
    component.form.controls['name'].setValue('');
    component.form.controls['categories'].setValue('');

    component.onSubmit();

    expect(mockAdminService.updateItem).not.toHaveBeenCalled();
  });

  it('should call updateItem and emit itemUpdated on successful update', fakeAsync(() => {
    spyOn(window, 'alert');
    spyOn(component.itemUpdated, 'emit');

    component.item = {
      id: '123',
      name: 'Old Name',
      description: 'Old Desc',
      categories: ['OldCat']
    };

    component.ngOnInit();
    component.form.controls['name'].setValue('New Item');
    component.form.controls['description'].setValue('Updated Description');
    component.form.controls['categories'].setValue('Cat1, Cat2');

    mockAdminService.updateItem.and.returnValue(of({}));

    component.onSubmit();
    tick();

    expect(mockAdminService.updateItem).toHaveBeenCalledWith('123', {
      name: 'New Item',
      description: 'Updated Description',
      categories: ['Cat1', 'Cat2']
    });

    expect(window.alert).toHaveBeenCalledWith('Item updated successfully!');
    expect(component.itemUpdated.emit).toHaveBeenCalled();
  }));

  it('should show alert on updateItem failure', fakeAsync(() => {
    spyOn(window, 'alert');

    component.item = {
      id: '123',
      name: 'Item',
      description: 'Desc',
      categories: ['A']
    };

    component.ngOnInit();
    component.form.controls['name'].setValue('Item');
    component.form.controls['description'].setValue('Desc');
    component.form.controls['categories'].setValue('A');

    mockAdminService.updateItem.and.returnValue(throwError(() => new Error('Failed')));

    component.onSubmit();
    tick();

    expect(window.alert).toHaveBeenCalledWith('Failed to update item.');
  }));
});