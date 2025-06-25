import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private baseUrl = 'https://dummyjson.com/recipes';
  constructor(private httpClient:HttpClient) { }

  getAllRecipes(){
    return this.httpClient.get(this.baseUrl);
  }

}
