import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductAdminComponent } from './components/productAdmin/productAdmin.component';
import { ProductsComponent } from './components/products/products.component';
import { AuthGuard } from './_guard/auth-guard';

 const appRoutes : Routes = [
  { path: "products", component: ProductsComponent },
  { path: "admin", component: ProductAdminComponent, canActivate : [AuthGuard] },
  { path: "products/:productId", component: ProductsComponent },
  { path: "**", redirectTo: "products", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
