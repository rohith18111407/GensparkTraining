import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StaffDashboardComponent } from './staff-dashboard';
import { provideHttpClient } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { importProvidersFrom } from '@angular/core';
import { StaffService } from '../../services/staff.service';
import { of } from 'rxjs';
import { StaffLeftSidebarComponent } from '../left-sidebar/left-sidebar';
import { StaffCenterContentComponent } from '../center-content/center-content';
import { StaffRightSidebarComponent } from '../right-sidebar/right-sidebar';

describe('StaffDashboardComponent', () => {
  let component: StaffDashboardComponent;
  let fixture: ComponentFixture<StaffDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        StaffLeftSidebarComponent,
        StaffCenterContentComponent,
        StaffRightSidebarComponent
      ],
      providers: [
        {
          provide: StaffService,
          useValue: {
            getUserProfile: () => of({ name: 'Test User' })
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(StaffDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the staff dashboard component', () => {
    expect(component).toBeTruthy();
  });

  it('should have default view as dashboard', () => {
    expect(component.selectedView).toBe('dashboard');
  });

  it('should set the selected view correctly', () => {
    component.setView('files');
    expect(component.selectedView).toBe('files');

    component.setView('users');
    expect(component.selectedView).toBe('users');
  });
});