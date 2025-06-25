import { TestBed } from '@angular/core/testing';

import { RecipeService } from './recipe-service';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import { provideHttpClient } from '@angular/common/http';

describe('RecipeService', () => {
  let service: RecipeService;
  let httpMock:HttpTestingController;

  beforeEach(() => {
      TestBed.configureTestingModule({
        imports:[],
        providers:[RecipeService,provideHttpClient(),provideHttpClientTesting()]
      });
      service = TestBed.inject(RecipeService);
      httpMock=TestBed.inject(HttpTestingController);
    });

  afterEach(()=>{
    httpMock.verify();
  });

  it('should retrieve recipes from API',()=>{
    const mockRecipes= {
      "recipes":[
        {
          "id": 1,
          "name": "Classic Margherita Pizza",
          "ingredients": [
            "Pizza dough",
            "Tomato sauce",
            "Fresh mozzarella cheese",
            "Fresh basil leaves",
            "Olive oil",
            "Salt and pepper to taste"
          ],
          "instructions": [
            "Preheat the oven to 475°F (245°C).",
            "Roll out the pizza dough and spread tomato sauce evenly.",
            "Top with slices of fresh mozzarella and fresh basil leaves.",
            "Drizzle with olive oil and season with salt and pepper.",
            "Bake in the preheated oven for 12-15 minutes or until the crust is golden brown.",
            "Slice and serve hot."
          ],
          "prepTimeMinutes": 20,
          
        }
      ]

    }
    //
    service.getAllRecipes().subscribe((res:any)=>{
      expect(res).toEqual(mockRecipes);
      expect(res.recipes?.length).toBe(1);
      expect(res.recipes[0].name).toBe("Classic Margherita Pizza");

    });

    const req = httpMock.expectOne("https://dummyjson.com/recipes");
    expect(req.request.method).toBe('GET');
    req.flush(mockRecipes);
  })

  
});
