import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileUploadContent } from './file-upload-content';

describe('FileUploadContent', () => {
  let component: FileUploadContent;
  let fixture: ComponentFixture<FileUploadContent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FileUploadContent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileUploadContent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
