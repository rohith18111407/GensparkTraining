import { Component, OnInit, signal } from '@angular/core';
import { User } from '../models/user.model';
import { ChartConfiguration } from 'chart.js';
import { UserService } from '../services/user.service';
// import { BaseChartDirective } from 'ng2-charts';
import { NgChartsModule } from 'ng2-charts';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  imports: [NgChartsModule,CommonModule, FormsModule,ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit{
  users : User[] = [];
  filteredUsers : User[] = [];
  filterText = '';
  addUserForm!: FormGroup;

  newUser : Partial<User> = {
    firstName : '',
    lastName : '',
    age : 0,
    gender : 'male',
    company_title_role : '',
    state : '' 
  }

  genderChartData: ChartConfiguration<'pie'>['data'] = {
    labels : ['Male','Female'],
    datasets : [
      {
        data : [0,0],
        label: 'Gender Pie Chart',
        backgroundColor: ['#4e79a7', '#f28e2c'],
        hoverOffset: 4
      }
    ]
  };

  roleChartData: ChartConfiguration<'bar'>['data'] = {
    labels : [],
    datasets : [
      {
        data : [],
        label : 'Users by Role',
        backgroundColor : '#76b7b2',
        borderWidth: 1 
      }
    ]
  }

  statesList : string[] = [];

  constructor(private userService: UserService)
  {

  }

  fetchUsers() : void {
    this.userService.getUsers().subscribe((users : any) => {
      console.log(users);
      const userList : User[] = users.users.map((u : any) => ({
        id: u.id,
        firstName: u.firstName,
        lastName: u.lastName,
        age: u.age,
        gender: u.gender,
        company_title_role: u.company?.title || 'Employee',
        state: u.address?.state || 'Unknown'
      }));
      
      this.users = userList;
      this.applyFilter();
    });
  }

  applyFilter() : void {
    const keyword = this.filterText.toLowerCase();
    const list = this.users.filter(user => 
      user.firstName.toLowerCase().includes(keyword) ||
      user.lastName.toLowerCase().includes(keyword) ||
      user.gender.toLowerCase().includes(keyword) ||
      user.company_title_role.toLowerCase().includes(keyword) ||
      user.state.toLowerCase().includes(keyword)
    );
    this.filteredUsers = list;
    this.updateCharts(list);
  }

  updateCharts(userList : User[]) : void {
    const genderCounts = {male:0, female:0};
    const roleCounts : {[key : string] : number} = {};
    const stateCounts : {[key : string] : number} = {};

    userList.forEach(user => {
      genderCounts[user.gender]++;
      roleCounts[user.company_title_role] = (roleCounts[user.company_title_role] || 0) + 1;
      stateCounts[user.state] = (stateCounts[user.state] || 0) + 1;
    });

    this.genderChartData.datasets[0].data = [
      genderCounts['male'],
      genderCounts['female']
    ];

    this.roleChartData.labels = Object.keys(roleCounts);
    this.roleChartData.datasets[0].data = Object.values(roleCounts);

    this.statesList = Object.entries(stateCounts).map(
      ([state, count]) => `${state} (${count})`
    );
  }

  onAddUser(): void {
      if (this.addUserForm.invalid) return;

      const newUser: User = {
        id: Date.now(),
        ...this.addUserForm.value
      };

      this.users.push(newUser);
      this.applyFilter();
      this.addUserForm.reset({ gender: 'male' });
    }

  // onAddUser() : void {
  //   this.userService.addUser(this.newUser).subscribe((createdUser) => {
  //     // Manually create a user with a fake ID and push it to local list
  //     const userToAdd: User = {
  //       id: Date.now(), // Mock ID
  //       firstName: this.newUser.firstName!,
  //       lastName: this.newUser.lastName!,
  //       age: this.newUser.age!,
  //       gender: this.newUser.gender!,
  //       company_title_role: this.newUser.company_title_role!,
  //       state: this.newUser.state!
  //     };

  //     this.users.push(userToAdd);
  //     this.applyFilter(); // Update charts and filteredUsers

  //     this.newUser = {
  //       firstName: '',
  //       lastName: '',
  //       age: 0,
  //       gender: 'male',
  //       company_title_role: '',
  //       state: ''
  //     };
  //   });
  // }

  initForm(): void {
    this.addUserForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      age: new FormControl(null, [Validators.required, Validators.min(1)]),
      gender: new FormControl('male', Validators.required),
      company_title_role: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required)
    });
  }

  get firstName() { return this.addUserForm.get('firstName'); }
  get lastName() { return this.addUserForm.get('lastName'); }
  get age() { return this.addUserForm.get('age'); }
  get gender() { return this.addUserForm.get('gender'); }
  get company_title_role() { return this.addUserForm.get('company_title_role'); }
  get state() { return this.addUserForm.get('state'); }


  ngOnInit(): void {
    this.initForm();
    this.fetchUsers();
  }
}
