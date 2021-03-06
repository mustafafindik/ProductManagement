import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './components/nav/nav.component';
import {MatGridListModule} from '@angular/material/grid-list';
import { ProductsComponent } from './components/products/products.component';
import {MatCardModule} from '@angular/material/card';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductAdminComponent } from './components/productAdmin/productAdmin.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatInputModule, } from "@angular/material/input";
import { MatTableModule } from "@angular/material/table";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorIntl, MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressSpinnerModule, } from "@angular/material/progress-spinner";
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar/';
import { MatDialogModule } from '@angular/material/dialog';
import { ProductDialogComponent } from './components/productAdmin/product-dialog/product-dialog.component';
import { AuthInterceptor } from './helper/AuthInterceptor';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [	
    AppComponent,
    NavComponent,
    ProductsComponent,
    ProductAdminComponent,
    ProductDialogComponent
      
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatGridListModule,
    MatCardModule,FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    FormsModule,
    MatIconModule,
    MatButtonModule,
    MatCheckboxModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatTooltipModule
  ],
  entryComponents: [
    ProductDialogComponent
  ],
  providers: [ { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule { }
