import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LeftSidebarComponent } from './left-sidebar';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

describe('LeftSidebarComponent', () => {
  let component: LeftSidebarComponent;
  let fixture: ComponentFixture<LeftSidebarComponent>;
  let debugElement: DebugElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeftSidebarComponent], // ✅ standalone component
    }).compileComponents();

    fixture = TestBed.createComponent(LeftSidebarComponent);
    component = fixture.componentInstance;
    debugElement = fixture.debugElement;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should emit viewChange on setView call', () => {
    spyOn(component.viewChange, 'emit');
    component.setView('files');
    expect(component.viewChange.emit).toHaveBeenCalledWith('files');
  });

  it('should emit viewChange on change call', () => {
    spyOn(component.viewChange, 'emit');
    component.change('items');
    expect(component.viewChange.emit).toHaveBeenCalledWith('items');
  });

  it('should emit correct view on Dashboard button click', () => {
    spyOn(component.viewChange, 'emit');
    const btn = debugElement.query(By.css('button'));
    btn.nativeElement.click();
    expect(component.viewChange.emit).toHaveBeenCalledWith('dashboard');
  });

  it('should emit correct view on Files button click', () => {
    spyOn(component.viewChange, 'emit');
    const btns = debugElement.queryAll(By.css('button'));
    btns[1].nativeElement.click(); // Files is 2nd
    expect(component.viewChange.emit).toHaveBeenCalledWith('files');
  });

  it('should emit correct view on Items button click', () => {
    spyOn(component.viewChange, 'emit');
    const btns = debugElement.queryAll(By.css('button'));
    btns[2].nativeElement.click(); // Items is 3rd
    expect(component.viewChange.emit).toHaveBeenCalledWith('items');
  });

  it('should emit correct view on Users button click', () => {
    spyOn(component.viewChange, 'emit');
    const btns = debugElement.queryAll(By.css('button'));
    btns[3].nativeElement.click(); // Users is 4th
    expect(component.viewChange.emit).toHaveBeenCalledWith('users');
  });

  it('should emit correct view on Statistics button click', () => {
    spyOn(component.viewChange, 'emit');
    const btns = debugElement.queryAll(By.css('button'));
    btns[4].nativeElement.click(); // Statistics is 5th
    expect(component.viewChange.emit).toHaveBeenCalledWith('statistics');
  });
});
