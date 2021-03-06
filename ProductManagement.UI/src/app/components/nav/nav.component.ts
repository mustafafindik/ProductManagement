import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService,private router: Router) { }
  loginUser: any = {};

  ngOnInit() {
  }

  login() {
    this.authService.login(this.loginUser);
  }

  logOut(){
    this.authService.logOut();
    this.router.navigate(["/products"]);
  }

  isAuthenticated(){
    return this.authService.loggedIn();
    
  }

  getUserName(){
   
    return this.authService.getUserName();
  }

  isAdmin(){ 
     
    return this.authService.getUserRoles().toString() =="Admin";
  }

}
