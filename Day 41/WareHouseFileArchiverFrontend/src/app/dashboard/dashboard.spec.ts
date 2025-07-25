import { ComponentFixture, fakeAsync, TestBed, tick } from "@angular/core/testing";
import { DashboardComponent } from "./dashboard";
import { DashboardService } from "../services/dashboard.service";
import { of, throwError } from "rxjs";

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let dashboardServiceSpy: jasmine.SpyObj<DashboardService>;

  beforeEach(() => {
    dashboardServiceSpy = jasmine.createSpyObj('DashboardService', ['getAllFiles', 'getAllItems']);

    TestBed.configureTestingModule({
      imports: [DashboardComponent],
      providers: [
        { provide: DashboardService, useValue: dashboardServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
  });

  it('should create dashboard component', () => {
    expect(component).toBeTruthy();
  });

  it('should load dashboard data and generate charts', fakeAsync(() => {
    spyOn(sessionStorage, 'getItem').and.returnValue(JSON.stringify({ username: 'test' }));
    dashboardServiceSpy.getAllFiles.and.returnValue(of([]));
    dashboardServiceSpy.getAllItems.and.returnValue(of([]));

    component.ngOnInit();
    tick();
    expect(component.username).toBe('test');
    expect(component.itemCards).toEqual([]);
    expect(component.pieData).toEqual([]);
  }));

  it('should handle dashboard load error', fakeAsync(() => {
    dashboardServiceSpy.getAllFiles.and.returnValue(throwError(() => new Error()));
    dashboardServiceSpy.getAllItems.and.returnValue(of([]));

    component.loadDashboardData();
    tick();
    expect(component.error).toBe('Failed to load dashboard data.');
  }));
});
