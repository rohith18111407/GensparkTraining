import { Injectable } from '@angular/core';
import { userModel } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class authService {
  private usersList: userModel[] = [
    { username: 'admin', password: 'admin@123', role: 'Admin' },
    { username: 'staff', password: 'staff@123', role: 'Staff' }
  ];

  isBrowser() {
    return typeof window !== 'undefined';
  }

  login(username: string, password: string): userModel | null {
    const user = this.usersList.find(
      u => u.username === username && u.password === password
    );
    if (user && this.isBrowser()) {
      sessionStorage.setItem('loggedInUser', JSON.stringify(user));
      localStorage.setItem('loggedInUser', JSON.stringify(user));
    }
    return user ?? null;
  }

  getLoggedInUser(): userModel | null {
    if (this.isBrowser()) {
      const userJson = sessionStorage.getItem('loggedInUser');
    //   const userJson = localStorage.getItem('loggedInUser');
      return userJson ? JSON.parse(userJson) : null;
    }
    return null;
  }

  logout() {
    if (this.isBrowser()) sessionStorage.removeItem('loggedInUser');
  }
}
