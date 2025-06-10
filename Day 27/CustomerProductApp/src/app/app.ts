import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CustomerDetails } from "./customer-details/customer-details";
import { ProductList } from "./product-list/product-list";

@Component({
  selector: 'app-root',
  imports: [CustomerDetails, ProductList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'CustomerProductApp';
}
