import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ProductModel } from '../models/products';
import { ProductService } from '../services/ProductService';
import { CurrencyPipe } from '@angular/common';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
    @Input() product : ProductModel | null = new ProductModel();
    @Output() addToCart:EventEmitter<Number> = new EventEmitter<Number>();
    private productService = inject(ProductService);
    private router = inject(Router);
 
    constructor() {
      // this.productService.getProduct(1).subscribe(
      //   {
      //     next : (data) => {
      //       this.product = data as ProductModel;
      //       console.log(this.product)
      //     },
      //     error : (err) => {
      //       console.log(err)
      //     },
      //     complete : () => {
      //       console.log("All done");
      //     }
      //   }
      // )
      
    }
 
    handleBuyClick(pid:Number|undefined){
    if(pid)
    {
        this.addToCart.emit(pid);
    }
  }

  showInfo(pid:number | undefined){
    if(pid){
        const username = localStorage.getItem('username');
        if(username)
          this.router.navigate(['home',username,'products',pid]);
    }
  }
}