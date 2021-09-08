import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model :any={};
  constructor(public authService:AuthService, private alertify:AlertifyService,private routor:Router) { }

  ngOnInit() {
  }
 login(){
   this.authService.login(this.model).subscribe(
     // void يعني برجعش داتا زيها زيnext 
     next=>{this.alertify.success("تم الدخول بنجاح")},
     error=>{this.alertify.error(error)},
     ()=>{this.routor.navigate(['/members']);}
   )
 }

 LoggedIn(){
  //const token = localStorage.getItem('token');
  //return !! token;
   return this.authService.LoggedIn();
 
 }
 LoggedOut(){
localStorage.removeItem('token');
this.alertify.message('تم الخروج');
this.routor.navigate(['/home']);




 }
  
}
