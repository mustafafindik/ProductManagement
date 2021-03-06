import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import {MatGridListModule} from '@angular/material/grid-list';
import { ProductsComponent } from './components/products/products.component';
import {MatCardModule} from '@angular/material/card';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductAdminComponent } from './components/productAdmin/productAdmin.component';
import { AuthGuard } from './_guard/auth-guard';

@NgModule({
  declarations: [	
    AppComponent,
    NavComponent,
    ProductsComponent,
    ProductAdminComponent
      
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatGridListModule,
    MatCardModule,FormsModule,
    ReactiveFormsModule,



  ],
  
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
