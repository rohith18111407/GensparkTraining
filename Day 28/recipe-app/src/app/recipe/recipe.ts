import { Component, inject, Inject, Input } from '@angular/core';
import { RecipeModel } from '../models/recipe.model';
import { RecipeService } from '../services/recipe.service';

@Component({
  selector: 'app-recipe',
  imports: [],
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe {
  @Input() recipe:RecipeModel | null = new RecipeModel();
  private recipeService = inject(RecipeService);

  // constructor()
  // {
  //   this.recipeService.getRecipe(1).subscribe(
  //     {
  //       next : (data) => {
  //         this.recipe = data as RecipeModel;
  //         console.log(this.recipe);
  //       },
  //       error : (err) => {
  //         console.log(err);
  //       },
  //       complete : () => {
  //         console.log("All done");
  //       }
  //     }
  //   )
  // }

}
