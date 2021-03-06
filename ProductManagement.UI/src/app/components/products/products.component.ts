import { Component, OnInit } from '@angular/core';
import { Settings } from 'src/app/helper/Settings';
import { Product } from 'src/app/model/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products!: Product[];
  path = Settings.ApiBaseUrl;
  searchTerm!: string;
  term!: string;

  constructor(private productService:ProductService) { }

  ngOnInit() {
    this.LoadData();
    
  }

  LoadData() {
    this.productService.getProducts().subscribe(data => {
     this.products = data;
      console.log(data);
      
    });
  }
}
