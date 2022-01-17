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

  brandIdSelected = 0;
  typeIdSelected = 0;

  sortSelected = 'name';
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProducts() {
    this.shopService
      .getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected)
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

  onSortSelected(sort: string) {
    this.sortSelected = sort;
    this.getProducts();
  }
}
