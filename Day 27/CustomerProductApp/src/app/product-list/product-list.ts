import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css'
})
export class ProductList {
  cartCount =0;

  products = [
    {
      id: 1,
      name: 'MacBook Pro',
      price: 150000,
      imageUrl : 'https://storage.googleapis.com/alpine-inkwell-325917.appspot.com/devices/macbook-pro-m1-14-header.png'
    },
    {
      id: 2,
      name: 'Samsung S24 Ultra',
      price: 160000,
      imageUrl : 'https://images.samsung.com/is/image/samsung/p6pim/hk_en/2401/gallery/hk-en-galaxy-s24-s928-489657-sm-s9280zogtgy-539359253?$624_624_PNG$'
    },
    {
      id: 3,
      name: 'JBL Tune 770NC',
      price: 5999,
      imageUrl : 'https://in.jbl.com/dw/image/v2/BFND_PRD/on/demandware.static/-/Sites-masterCatalog_Harman/default/dwca9d65c5/1.JBL_Tune_770NC_Product%20Image_Hero_Black.png?sw=535&sh=535'
    }
  ];

  addToCart()
  {
    this.cartCount++;
  }
}
