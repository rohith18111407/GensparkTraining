import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Recipe } from "./recipe/recipe";
import { Recipes } from './recipes/recipes';

@Component({
  selector: 'app-root',
  imports: [Recipes],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'recipe-app';
}
