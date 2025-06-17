import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductModel } from '../models/products';
import { ProductService } from '../services/ProductService';
import { CurrencyPipe } from '@angular/common';
import { debounce, debounceTime, delay } from 'rxjs';

@Component({
  selector: 'app-product-detail',
  imports: [CurrencyPipe],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.css'
})
export class ProductDetail implements OnInit{
  
  product:ProductModel | null = null;
  loading=true;
  error=false;

  
  private route=inject(ActivatedRoute);
  private productService = inject(ProductService);
  private router = inject(Router);


  ngOnInit(): void {
    const id = this.route.snapshot.params["id"] as number;
    console.log(`product id = ${id}`);
    if(id){
      this.productService.getProduct(id)
      .pipe(
        debounceTime(300),
        delay(500),
      )
      .subscribe({
        next:(data)=>{
          this.product=data as ProductModel;
        },
        error:(err)=>{
          console.log('Error fetching product',err);
        }
      })
    }

  }



}
