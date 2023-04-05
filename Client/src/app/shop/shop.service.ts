import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, Type } from '@angular/core';
import { retry } from 'rxjs';
import { Brand } from '../shared/models/brand';
import { pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { type } from '../shared/models/type';
import { ShopParams } from '../shared/models/ShopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https:localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(ShopParams : ShopParams) {
  
    let params = new HttpParams ();

    if (ShopParams.brandId > 0) params = params.append('brandId',ShopParams.brandId);
    if (ShopParams.typeId)  params  = params.append('typeId ',ShopParams.typeId);
     params  = params.append('sort ', ShopParams.sort);
     params  = params.append('PageIndex ', ShopParams.pageNumber);
     params  = params.append('PageSize ', ShopParams.pageSize);
     if (ShopParams.search) params =params.append('search',ShopParams.search);

    return this.http.get<pagination<Product[]>>(this.baseUrl + 'products',{params});
  }


  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }

   getTypes(){
    return this.http.get<type[]>(this.baseUrl + 'products/types');
   }
}
