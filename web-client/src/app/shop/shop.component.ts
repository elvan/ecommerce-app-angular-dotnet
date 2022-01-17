import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';
import { ShopParams } from '../shared/models/shop-params';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef | undefined;

  products: IProduct[] = [];
  brands: IProductBrand[] = [];
  types: IProductType[] = [];

  shopParams = new ShopParams();

  totalCount = 0;

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

  getProducts(): void {
    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        if (response) {
          this.products = response.data;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalCount = response.count;
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getProductBrands(): void {
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

  getProductTypes(): void {
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

  onBrandSelected(brandId: number): void {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string): void {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(page: number): void {
    this.shopParams.pageNumber = page;
    this.getProducts();
  }

  onSearch() {
    if (this.searchTerm) {
      this.shopParams.search = this.searchTerm.nativeElement.value;
      this.getProducts();
    }
  }

  onReset(): void {
    if (this.searchTerm) {
      this.searchTerm.nativeElement.value = '';
      this.shopParams = new ShopParams();
      this.getProducts();
    }
  }
}
