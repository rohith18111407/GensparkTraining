import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StaffLeftSidebarComponent } from './left-sidebar';

describe('StaffLeftSidebarComponent', () => {
  let component: StaffLeftSidebarComponent;
  let fixture: ComponentFixture<StaffLeftSidebarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [StaffLeftSidebarComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(StaffLeftSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit viewChange when setView is called', () => {
    spyOn(component.viewChange, 'emit');
    component.setView('files');
    expect(component.viewChange.emit).toHaveBeenCalledWith('files');
  });

  it('should emit viewChange when change is called', () => {
    spyOn(component.viewChange, 'emit');
    component.change('users');
    expect(component.viewChange.emit).toHaveBeenCalledWith('users');
  });
});