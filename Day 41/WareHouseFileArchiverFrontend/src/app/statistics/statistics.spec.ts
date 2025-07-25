import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StatisticsComponent } from './statistics';
import { StatisticsService } from '../services/statistics.service';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('StatisticsComponent', () => {
  let component: StatisticsComponent;
  let fixture: ComponentFixture<StatisticsComponent>;
  let statisticsServiceSpy: jasmine.SpyObj<StatisticsService>;

  const mockFileStats = [
    { extension: '.pdf', totalSizeMB: 50 },
    { extension: '.docx', totalSizeMB: 30 },
  ];

  const mockUploadDownloadData = {
    days: ['Mon', 'Tue'],
    uploads: [5, 10],
    downloads: [3, 7],
  };

  const mockRecentActivities = [
    { user: 'Admin1', action: 'uploaded', target: 'File A', timestamp: new Date() },
    { user: 'Staff1', action: 'downloaded', target: 'File B', timestamp: new Date() },
  ];

  const mockRecentFiles = [
    {
      fileName: 'report',
      fileExtension: '.pdf',
      sizeMB: 2.5,
      createdBy: 'Admin1',
      createdAt: new Date()
    }
  ];

  const mockRecentItems = [
    {
      name: 'Item 1',
      categories: ['Finance'],
      createdBy: 'Admin1',
      createdAt: new Date()
    }
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('StatisticsService', [
      'getFileExtensionStats',
      'getUploadDownloadTrends',
      'getRecentActivities',
      'getRecentFiles',
      'getRecentItems'
    ]);

    await TestBed.configureTestingModule({
      imports: [StatisticsComponent], // Standalone component
      providers: [{ provide: StatisticsService, useValue: spy }]
    }).compileComponents();

    statisticsServiceSpy = TestBed.inject(StatisticsService) as jasmine.SpyObj<StatisticsService>;

    statisticsServiceSpy.getFileExtensionStats.and.returnValue(of(mockFileStats));
    statisticsServiceSpy.getUploadDownloadTrends.and.returnValue(of(mockUploadDownloadData));
    statisticsServiceSpy.getRecentActivities.and.returnValue(of(mockRecentActivities));
    statisticsServiceSpy.getRecentFiles.and.returnValue(of(mockRecentFiles));
    statisticsServiceSpy.getRecentItems.and.returnValue(of(mockRecentItems));

    fixture = TestBed.createComponent(StatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch and assign fileStats correctly', () => {
    expect(component.fileStats.length).toBe(2);
    expect(component.fileStats[0].extension).toBe('.pdf');
  });

  it('should generate pieChartData with correct labels and data', () => {
    const pieData = component.pieChartData.data;
    expect(pieData.labels).toEqual(['.pdf', '.docx']);
    expect(pieData.datasets[0].data).toEqual([50, 30]);
  });

  it('should generate lineChartData with correct labels and dataset values', () => {
    const lineData = component.lineChartData.data;
    expect(lineData.labels).toEqual(['Mon', 'Tue']);
    expect(lineData.datasets[0].data).toEqual([5, 10]); // uploads
    expect(lineData.datasets[1].data).toEqual([3, 7]);  // downloads
  });

  it('should render recent activity list correctly', () => {
    const listItems = fixture.debugElement.queryAll(By.css('ul.list-group li'));
    expect(listItems.length).toBe(2);
    expect(listItems[0].nativeElement.textContent).toContain('Admin1');
    expect(listItems[0].nativeElement.textContent).toContain('uploaded');
  });

  it('should render recent files table with correct values', async () => {
    await fixture.whenStable();
    fixture.detectChanges();

    const fileRows = fixture.debugElement.queryAll(By.css('#recent-files tbody tr'));
    expect(fileRows.length).toBe(1);

    const rowText = fileRows[0].nativeElement.textContent;
    expect(rowText).toContain('report.pdf');
    expect(rowText).toContain('2.5');
    expect(rowText).toContain('Admin1');
  });

  it('should render recent items table with correct values', async () => {
    await fixture.whenStable();
    fixture.detectChanges();

    const itemRows = fixture.debugElement.queryAll(By.css('#recent-items tbody tr'));
    expect(itemRows.length).toBe(1);

    const rowText = itemRows[0].nativeElement.textContent;
    expect(rowText).toContain('Item 1');
    expect(rowText).toContain('Finance');
    expect(rowText).toContain('Admin1');
  });
});
