import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  baseUrl = 'https://localhost:44301/api/auth/';
  decodedToken:any;
constructor(private http:HttpClient) { }

login(model: any) {
  //pipe  تم استخدامه لان هناك قيمة راجعة
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      if (user) { 
        localStorage.setItem('token', user.token);
        this.decodedToken=this.jwtHelper.decodeToken(user.token);
        console.log(this.decodedToken);
      }
    }))
}

register(model :any){
  //model  يقصد به  الجيسون ابجكت المرسل
  return this.http.post(this.baseUrl+'Register',model);

}

LoggedIn(){
  try{
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
  catch{
    return false;
  }
}
}
