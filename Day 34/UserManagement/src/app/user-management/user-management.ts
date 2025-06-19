import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged, startWith, Subject, combineLatest } from 'rxjs';
import { bannedWordsValidator } from '../custom-validators/banned-words.validator';
import { passwordStrengthValidator } from '../custom-validators/password-strength.validator';
import { confirmPasswordValidator } from '../custom-validators/confirm-password.validator';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';
import { CommonModule, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-user-management',
  imports : [FormsModule,ReactiveFormsModule, NgIf,NgFor,CommonModule],
  templateUrl: './user-management.html',
  styleUrls: ['./user-management.css']
})
export class UserManagementComponent implements OnInit {
  userForm: FormGroup;
  searchInput$ = new Subject<string>();
  users: User[] = [];
  filteredUsers: User[] = [];
  selectedRole = 'All';

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.userForm = this.fb.group({
      username: ['', [Validators.required, bannedWordsValidator(['admin', 'root'])]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, passwordStrengthValidator()]],
      confirmPassword: ['', Validators.required],
      role: ['User', Validators.required]
    }, { validators: confirmPasswordValidator });
  }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
      this.applyFilter('');
    });

    combineLatest([
      this.searchInput$.pipe(startWith(''), debounceTime(300), distinctUntilChanged()),
      this.userService.users$
    ]).subscribe(([searchTerm, users]) => {
      const filtered = users.filter(user => {
        const matchesSearch = user.username.toLowerCase().includes(searchTerm.toLowerCase()) ||
                              user.role.toLowerCase().includes(searchTerm.toLowerCase());
        const matchesRole = this.selectedRole === 'All' || user.role === this.selectedRole;
        return matchesSearch && matchesRole;
      });
      this.filteredUsers = filtered;
    });
  }

  onSubmit(): void {
    if (this.userForm.invalid) return;
    const { username, email, password, role } = this.userForm.value;
    this.userService.addUser({ username, email, password, role });
    this.userForm.reset({ role: 'User' });
  }

  applyFilter(value: string): void {
    this.searchInput$.next(value);
  }

  onSearchInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.applyFilter(input.value);
  }

  onRoleChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.selectedRole = select.value;
    this.applyFilter('');
  }
}