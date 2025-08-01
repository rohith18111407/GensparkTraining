import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CenterContentComponent } from './center-content';
import { AdminService } from '../../services/admin.service';
import { of, throwError } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('CenterContentComponent', () => {
  let component: CenterContentComponent;
  let fixture: ComponentFixture<CenterContentComponent>;
  let adminServiceSpy: jasmine.SpyObj<AdminService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('AdminService', [
      'getAllItems',
      'deleteItem',
      'getAllUsers',
      'deleteUser',
      'getAllFiles',
      'deleteFile',
      'downloadFile'
    ]);

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule,CenterContentComponent],
      providers: [{ provide: AdminService, useValue: spy }],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(CenterContentComponent);
    component = fixture.componentInstance;
    adminServiceSpy = TestBed.inject(AdminService) as jasmine.SpyObj<AdminService>;
  });

  describe('ngOnChanges', () => {
    it('should fetch items when view is items', () => {
      component.view = 'items';
      spyOn(component, 'fetchItems');
      component.ngOnChanges();
      expect(component.fetchItems).toHaveBeenCalled();
    });

    it('should fetch users when view is users', () => {
      component.view = 'users';
      spyOn(component, 'fetchUsers');
      component.ngOnChanges();
      expect(component.fetchUsers).toHaveBeenCalled();
    });

    it('should fetch files when view is files', () => {
      component.view = 'files';
      spyOn(component, 'fetchFiles');
      component.ngOnChanges();
      expect(component.fetchFiles).toHaveBeenCalled();
    });
  });

  describe('Items', () => {
    it('should fetch items successfully', () => {
      const mockItems = [{ name: 'item1', categories: ['cat'] }];
      adminServiceSpy.getAllItems.and.returnValue(of(mockItems));

      component.fetchItems();
      expect(component.items).toEqual(mockItems);
      expect(component.loading).toBeFalse();
    });

    it('should handle item fetch error', () => {
      adminServiceSpy.getAllItems.and.returnValue(throwError(() => new Error('error')));

      component.fetchItems();
      expect(component.error).toBe('Failed to fetch items.');
      expect(component.loading).toBeFalse();
    });

    it('should toggle add item form', () => {
      component.toggleAddItemForm(true);
      expect(component.showAddItemForm).toBeTrue();
    });

    it('should apply item filters', () => {
      component.items = [
        { name: 'item1', categories: ['cat'] },
        { name: 'foo', categories: ['bar'] }
      ];
      component.itemSearchTerm = 'cat';
      component.applyItemFilters();
      expect(component.filteredItems.length).toBe(1);
    });

    it('should toggle edit item form', () => {
      const item = { name: 'item' };
      component.toggleEditItemForm(item);
      expect(component.selectedItemToEdit).toBe(item);
      expect(component.showEditItemForm).toBeTrue();
    });

    it('should change item sort and fetch', () => {
      spyOn(component, 'fetchItems');
      component.sortBy = 'name';
      component.changeSort('name');
      expect(component.isDescending).toBeTrue();
      expect(component.fetchItems).toHaveBeenCalled();
    });

    it('should confirm and delete item', () => {
      spyOn(window, 'confirm').and.returnValue(true);
      spyOn(window, 'alert');
      adminServiceSpy.deleteItem.and.returnValue(of({}));
      spyOn(component, 'fetchItems');

      component.confirmDelete('123');
      expect(adminServiceSpy.deleteItem).toHaveBeenCalledWith('123');
      expect(component.fetchItems).toHaveBeenCalled();
    });
  });

  describe('Users', () => {
    it('should fetch users successfully', () => {
      const mockUsers = [{ username: 'john', roles: ['Admin'] }];
      adminServiceSpy.getAllUsers.and.returnValue(of(mockUsers));
      component.fetchUsers();
      expect(component.users).toEqual(mockUsers);
      expect(component.filteredUsers.length).toBe(1);
    });

    it('should filter users by role and name', () => {
      component.users = [
        { username: 'john', roles: ['Admin'] },
        { username: 'jane', roles: ['Staff'] }
      ];
      component.userSearchTerm = 'john';
      component.selectedRoleFilter = 'Admin';
      component.applyUserFilters();
      expect(component.filteredUsers.length).toBe(1);
    });

    it('should toggle add user form', () => {
      component.toggleAddUserForm(true);
      expect(component.showAddUserForm).toBeTrue();
    });

    it('should confirm and delete user', () => {
      spyOn(window, 'confirm').and.returnValue(true);
      spyOn(window, 'alert');
      adminServiceSpy.deleteUser.and.returnValue(of({}));
      spyOn(component, 'fetchUsers');

      component.confirmDeleteUser('456');
      expect(adminServiceSpy.deleteUser).toHaveBeenCalledWith('456');
      expect(component.fetchUsers).toHaveBeenCalled();
    });

    it('should toggle edit user form', () => {
      const user = { username: 'test' };
      component.toggleEditUserForm(user);
      expect(component.selectedUserToEdit).toBe(user);
      expect(component.showEditUserForm).toBeTrue();
    });
  });

  describe('Files', () => {
    const mockFiles = [
      {
        id: '1',
        fileName: 'report',
        fileExtension: '.pdf',
        createdAt: new Date().toISOString(),
        itemName: 'item',
        versionNumber: 1,
        category: 'Reports',
        fileSizeInBytes: 1024,
        createdBy: 'admin'
      }
    ];

    it('should fetch files successfully and group them', () => {
      adminServiceSpy.getAllFiles.and.returnValue(of(mockFiles));
      component.fetchFiles();
      expect(component.files).toEqual(mockFiles);
      expect(component.groupedFiles['Reports'].length).toBe(1);
    });

    it('should confirm and delete file', () => {
      spyOn(window, 'confirm').and.returnValue(true);
      spyOn(window, 'alert');
      adminServiceSpy.deleteFile.and.returnValue(of({ message: 'done' }));
      spyOn(component, 'fetchFiles');

      component.confirmDeleteFile('1');
      expect(adminServiceSpy.deleteFile).toHaveBeenCalled();
      expect(component.fetchFiles).toHaveBeenCalled();
    });

    it('should return formatted size correctly', () => {
      expect(component.getFormattedSize(512)).toBe('512 B');
      expect(component.getFormattedSize(2048)).toContain('2.00 KB');
      expect(component.getFormattedSize(2 * 1024 * 1024)).toContain('2.00 MB');
    });

    it('should download a file', () => {
      const blob = new Blob(['dummy content']);
      adminServiceSpy.downloadFile.and.returnValue(of(blob));
      spyOn(document, 'createElement').and.callThrough();

      const mockFile = {
        fileName: 'report',
        fileExtension: '.pdf',
        versionNumber: 1
      };

      component.onDownloadFile(mockFile);
      expect(adminServiceSpy.downloadFile).toHaveBeenCalled();
    });
  });
});