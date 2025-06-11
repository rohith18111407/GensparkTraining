import { Component, inject, OnInit, signal } from '@angular/core';
import { RecipeModel } from '../models/recipe.model';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../recipe/recipe';

@Component({
  selector: 'app-recipes',
  imports: [Recipe],
  templateUrl: './recipes.html',
  styleUrl: './recipes.css'
})
export class Recipes implements OnInit{
  // recipes: RecipeModel[] | undefined = undefined;
  private recipeService = inject(RecipeService);

  recipes = signal<RecipeModel[]>([]);

  ngOnInit(): void {
    this.recipeService.getAllRecipes().subscribe(
      {
        next : (data : any) => {
           this.recipes.set(data.recipes as RecipeModel[]);
        },
        error : (err) => {
          console.log(err);
        },
        complete : () =>{
        }
      }
    )
  }
}
