import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable({
    providedIn: 'root' // ADDED providedIn root here.
  })
export class AuthGuard implements CanActivate {

    constructor(private authService :AuthService, private router: Router){}

    canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): boolean {
        var role = this.authService.getUserRoles();
         if(role=="Admin"){
             return true;
         }
         this.router.navigate(["/products"]);
         return false;
    }
}
