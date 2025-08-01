import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AddFileComponent } from './add-file';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';

describe('AddFileComponent', () => {
  let component: AddFileComponent;
  let fixture: ComponentFixture<AddFileComponent>;
  let mockAdminService: any;

  beforeEach(async () => {
    mockAdminService = {
      getAllItems: jasmine.createSpy('getAllItems'),
      uploadFile: jasmine.createSpy('uploadFile')
    };

    await TestBed.configureTestingModule({
      imports: [
        AddFileComponent,
        ReactiveFormsModule,
        FormsModule
      ],
      providers: [{ provide: AdminService, useValue: mockAdminService }]
    }).compileComponents();

    fixture = TestBed.createComponent(AddFileComponent);
    component = fixture.componentInstance;
  });

  it('should create the component and initialize form', () => {
    expect(component).toBeTruthy();
    expect(component.fileForm).toBeDefined();
    expect(component.fileForm.valid).toBeFalse();
  });

  it('should load items on ngOnInit', () => {
    const items = [
      { name: 'Item A', categories: ['Cat 1', 'Cat 2'] },
      { name: 'Item B', categories: ['Cat 2'] }
    ];
    mockAdminService.getAllItems.and.returnValue(of(items));

    component.ngOnInit();

    expect(mockAdminService.getAllItems).toHaveBeenCalled();
    expect(component.allItems).toEqual(items);
    expect(component.itemNames).toEqual(['Item A', 'Item B']);
  });

  it('should handle getAllItems error', () => {
    spyOn(window, 'alert');
    mockAdminService.getAllItems.and.returnValue(throwError(() => 'error'));
    component.ngOnInit();
    expect(window.alert).toHaveBeenCalledWith('Failed to load items.');
  });

  it('should update filteredCategories when item name changes', () => {
    component.allItems = [
      { name: 'Item A', categories: ['Cat 1'] },
      { name: 'Item A', categories: ['Cat 2'] }
    ];
    component.fileForm.patchValue({ itemName: 'Item A' });

    component.onItemNameChange();

    expect(component.filteredCategories).toEqual(['Cat 1', 'Cat 2']);
    expect(component.fileForm.value.itemCategory).toBe('');
  });

  it('should set file when file is selected', () => {
    const file = new File(['content'], 'test.txt', { type: 'text/plain' });
    const event = { target: { files: [file] } };

    component.onFileChange(event);

    expect(component.fileToUpload).toBe(file);
    expect(component.fileForm.value.file).toBe(file);
  });

  it('should show alert if form is invalid on submit', () => {
    spyOn(window, 'alert');
    component.fileToUpload = null;

    component.submit();

    expect(window.alert).toHaveBeenCalledWith('Please complete all required fields.');
  });

  it('should emit cancel if user confirms', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    spyOn(component.cancel, 'emit');

    component.onCancel();

    expect(window.confirm).toHaveBeenCalled();
    expect(component.cancel.emit).toHaveBeenCalled();
  });

  it('should not emit cancel if user declines', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    spyOn(component.cancel, 'emit');

    component.onCancel();

    expect(component.cancel.emit).not.toHaveBeenCalled();
  });
});