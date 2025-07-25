import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StaffCenterContentComponent } from './center-content';
import { StaffService } from '../../services/staff.service';
import { of, throwError } from 'rxjs';

describe('StaffCenterContentComponent', () => {
  let component: StaffCenterContentComponent;
  let fixture: ComponentFixture<StaffCenterContentComponent>;
  let staffServiceMock: any;

  beforeEach(() => {
    staffServiceMock = {
      getAllItems: jasmine.createSpy('getAllItems').and.returnValue(of([])),
      getAllUsers: jasmine.createSpy('getAllUsers').and.returnValue(of([])),
      getAllFiles: jasmine.createSpy('getAllFiles').and.returnValue(of([])),
      downloadFile: jasmine.createSpy('downloadFile').and.returnValue(of(new Blob(['test'], { type: 'application/octet-stream' })))
    };

    TestBed.configureTestingModule({
      imports: [StaffCenterContentComponent],
      providers: [{ provide: StaffService, useValue: staffServiceMock }]
    }).compileComponents();

    fixture = TestBed.createComponent(StaffCenterContentComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch items on items view', () => {
    component.view = 'items';
    component.ngOnChanges();
    expect(staffServiceMock.getAllItems).toHaveBeenCalled();
  });

  it('should fetch users on users view', () => {
    component.view = 'users';
    component.ngOnChanges();
    expect(staffServiceMock.getAllUsers).toHaveBeenCalled();
  });

  it('should fetch files on files view', () => {
    component.view = 'files';
    component.ngOnChanges();
    expect(staffServiceMock.getAllFiles).toHaveBeenCalled();
  });

  it('should apply file filters and sort', () => {
    component.files = [
      { fileName: 'test.pdf', itemName: 'item', category: 'docs', createdAt: new Date().toISOString() }
    ];
    component.fileSearchTerm = 'test';
    component.applyFileFilters();
    expect(Object.keys(component.groupedFiles)).toContain('docs');
  });

  it('should handle file download success', () => {
    spyOn(window, 'alert');
    const file = { fileName: 'sample', versionNumber: 1, fileExtension: '.txt' };
    component.onDownloadFile(file);
    expect(staffServiceMock.downloadFile).toHaveBeenCalled();
  });

  it('should handle download error', () => {
    staffServiceMock.downloadFile.and.returnValue(throwError(() => new Error('fail')));
    spyOn(window, 'alert');
    const file = { fileName: 'sample', versionNumber: 1, fileExtension: '.txt' };
    component.onDownloadFile(file);
    expect(window.alert).toHaveBeenCalledWith('Failed to download file.');
  });
});