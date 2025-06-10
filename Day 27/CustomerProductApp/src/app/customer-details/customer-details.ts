import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-details',
  imports: [],
  templateUrl: './customer-details.html',
  styleUrl: './customer-details.css'
})
export class CustomerDetails {
  customer = {
    id: 1,
    name: 'Rohith',
    email: 'rohith@customerproductapp.com',
    location: 'Tamil Nadu, India'
  }

  likes = 0;
  dislikes = 0;

  like()
  {
    this.likes++;
  }

  dislike()
  {
    this.dislikes++;
  }
}
