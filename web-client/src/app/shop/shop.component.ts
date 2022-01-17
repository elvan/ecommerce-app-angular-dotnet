import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: IProduct[] = [];
  brands: IProductBrand[] = [];
  types: IProductType[] = [];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe(
      (value) => {
        this.products = value.data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe(
      (value) => {
        this.brands = value;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getProductTypes() {
    this.shopService.getProuctTypes().subscribe(
      (value) => {
        this.types = value;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
