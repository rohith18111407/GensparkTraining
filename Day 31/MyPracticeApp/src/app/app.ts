import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Products } from "./products/products";
import { Navigation } from "./navigation/navigation";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Products, Navigation],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'MyPracticeApp';
}
