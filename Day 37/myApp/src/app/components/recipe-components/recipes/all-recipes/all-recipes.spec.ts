import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';

import { AllRecipes } from './all-recipes';
import { RecipeService } from '../../../../services/recipe-service';
import { Component } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { of } from 'rxjs';

// @Component({
//   selector:'app-all-recipes',
//   template:''
// })
// class MockRecipesComponent{}

describe('AllRecipes  Component (Standalone)', () => {
  let component: AllRecipes;
  let fixture: ComponentFixture<AllRecipes>;
  let mockRecipeService : jasmine.SpyObj<RecipeService>;

  const mockResponse = {
    recipes:[
      {
        id:1,
        name:"Classic Margherita Pizza",
        ingredients:[
        "Pizza dough",
        "Tomato sauce",
        "Fresh mozzarella cheese",
        "Fresh basil leaves",
        "Olive oil",
        "Salt and pepper to taste"
        ],
        instructions:[
        "Preheat the oven to 475°F (245°C).",
        "Roll out the pizza dough and spread tomato sauce evenly.",
        "Top with slices of fresh mozzarella and fresh basil leaves.",
        "Drizzle with olive oil and season with salt and pepper.",
        "Bake in the preheated oven for 12-15 minutes or until the crust is golden brown.",
        "Slice and serve hot."
        ],cuisine:"Italian"
      },
      {
        id: 2,
        name: "Vegetarian Stir-Fry",
        ingredients: [
          "Tofu, cubed",
          "Broccoli florets",
          "Carrots, sliced",
          "Bell peppers, sliced",
          "Soy sauce",
          "Ginger, minced",
          "Garlic, minced",
          "Sesame oil",
          "Cooked rice for serving"
        ],
        instructions: [
          "In a wok, heat sesame oil over medium-high heat.",
          "Add minced ginger and garlic, sauté until fragrant.",
          "Add cubed tofu and stir-fry until golden brown.",
          "Add broccoli, carrots, and bell peppers. Cook until vegetables are tender-crisp.",
          "Pour soy sauce over the stir-fry and toss to combine.",
          "Serve over cooked rice."
        ],
        cuisine:"Asian"
      }
    ]
  };


  beforeEach(async () => {
      const spy = jasmine.createSpyObj('RecipeService',['getAllRecipes']);
      await TestBed.configureTestingModule({
        imports:[],
        providers:[provideHttpClient(),
          provideHttpClientTesting(),
          {provide : RecipeService,useValue: spy}
        ],
      }).compileComponents();

      fixture=TestBed.createComponent(AllRecipes);
      component=fixture.componentInstance;
      mockRecipeService=TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;

  });

  it('it should create the component',()=>{
      expect(component).toBeTruthy();
  })

  it('should load recipes on init',fakeAsync(()=>{
    mockRecipeService.getAllRecipes.and.returnValue(of(mockResponse));

    fixture.detectChanges();
    tick(5000);
    expect(component.recipes.length).toBe(2);
    expect(component.recipes[0].name).toBe('Classic Margherita Pizza');
  }));


});
