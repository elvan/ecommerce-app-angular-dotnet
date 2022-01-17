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

  brandIdSelected!: number;
  typeIdSelected!: number;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService
      .getProducts(this.brandIdSelected, this.typeIdSelected)
      .subscribe(
        (value) => {
          if (value) {
            this.products = value.data;
          }
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getProductBrands() {
    this.shopService.getProductBrands().subscribe(
      (response) => {
        this.brands = [
          {
            id: 0,
            name: 'All',
          },
          ...response,
        ];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getProductTypes() {
    this.shopService.getProuctTypes().subscribe(
      (response) => {
        this.types = [
          {
            id: 0,
            name: 'All',
          },
          ...response,
        ];
      },
      (error) => {
        console.log(error);
      }
    );
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }
}
