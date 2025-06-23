import { Component, inject } from '@angular/core';
import { UserModel } from '../models/userModel';
import { userService } from '../services/userService';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile {
  _userService = inject(userService);
     profileData:UserModel = new UserModel();

     constructor(){
        this._userService.callGetProfile().subscribe({
          next:(data:any)=>{
            this.profileData = UserModel.fromForm(data);
            console.log(data);
          }
        })
     }

}
