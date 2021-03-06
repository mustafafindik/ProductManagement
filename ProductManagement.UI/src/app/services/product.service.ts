import { Injectable } from '@angular/core';
import { Settings } from '../helper/Settings';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Product } from '../model/product';
@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }
  path = Settings.ApiBaseUrl;
  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(this.path + "products");
  }

  getProductById(productId: number): Observable<Product> {
    return this.httpClient.get<Product>(this.path + "products/" + productId);
  }


  add(product: Product): Observable<any> {
    return this.httpClient.post(this.path + 'products/add', product, { observe: 'response' });
  }

  update(product: Product): Observable<any> {
    return this.httpClient.post(this.path + 'products/update', product, { observe: 'response' });
  }

  delete(product: Product): Observable<any> {
    return this.httpClient.post(this.path + 'products/delete' , product, { observe: 'response' });
  }

  
}



