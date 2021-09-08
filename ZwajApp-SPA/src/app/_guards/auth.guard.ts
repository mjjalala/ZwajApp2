import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/Auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authservice:AuthService,private router:Router,private alertify:AlertifyService) {}
  canActivate():  boolean {
    if(this.authservice.LoggedIn()){
      return true;
    }
    this.alertify.error('يجب تسجيل الدخول أولا');
    this.router.navigate(['']);
    return false;
  }
}
