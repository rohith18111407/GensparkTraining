import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { First } from "./first/first";
import { Product } from "./product/product";
import { Products } from "./products/products";
import { Login } from "./login/login";
import { Menu } from "./menu/menu";

@Component({
  selector: 'app-root',
  imports: [Products, Login, Menu],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'myApp';
}
