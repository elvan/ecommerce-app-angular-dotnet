import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPagination } from '../shared/models/pagination';
import { IProductBrand } from '../shared/models/product-brand';
import { IProductType } from '../shared/models/product-type';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(
    brandId?: number,
    typeId?: number,
    sort?: string
  ): Observable<IPagination | null> {
    let params = new HttpParams();

    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }

    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }

    if (sort) {
      params = params.append('sort', sort);
    }

    return this.http
      .get<IPagination | null>(this.baseUrl + 'products', {
        observe: 'response',
        params: params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getProductBrands() {
    return this.http.get<IProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getProuctTypes() {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }
}
