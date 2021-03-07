import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Settings } from 'src/app/helper/Settings';
import { Product, productImage } from 'src/app/model/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  @ViewChild('someNameInput')
  tabpane!: ElementRef;


  product!: Product;
  path = Settings.ApiBaseUrl;
  productImages!: productImage[];
  constructor(private _snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getProductById(params["productId"]);
    });

 
  }

  getProductById(productId:number) {
    this.productService.getProductById(productId).subscribe(data => {
      this.product = data;
      this.productImages=data.productImages;
     
    });
  }

  NotUsedError(){
    this._snackBar.open("Henüz Kullanılmıyor..", "Tamam", { duration: 5000, });
  }

}
