import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../../../../services/recipe-service';
import { RecipeModel } from '../../../../models/recipeModel';
import { debounceTime } from 'rxjs';
import { Recipe } from "../../recipe/recipe/recipe";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-all-recipes',
  imports: [Recipe,CommonModule],
  templateUrl: './all-recipes.html',
  styleUrl: './all-recipes.css'
})
export class AllRecipes implements OnInit{

  recipes:RecipeModel[]=[];
  errorMessage='';
  loading:boolean=false;

  constructor(private recipeService:RecipeService){}

  ngOnInit(): void {
    
    this.recipeService.getAllRecipes().pipe(
      debounceTime(3000)
    )
    .subscribe({
      next:(data:any)=>{
        // console.log(data.recipes);
        this.recipes=data.recipes.map((recipe:any)=>RecipeModel.fromResponse(recipe));
        console.log(this.recipes)
      }
    })
  }

  

}
