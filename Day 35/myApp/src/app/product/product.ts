import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ProductModel } from '../models/product';
import { ProductService } from '../services/product.service';
import { CurrencyPipe } from '@angular/common';

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
}
