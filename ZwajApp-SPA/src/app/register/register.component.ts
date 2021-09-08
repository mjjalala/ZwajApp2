import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/Auth.service';
import { error } from '@angular/compiler/src/util';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 
  // EventEmitter  مرسل الاحداث 
  @Output() CancelRegister =new EventEmitter();
  model: any = {};
  constructor(private authService:AuthService,private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
 this.authService.register(this.model).subscribe(
   // لانه مافي داتا راجعة فقط بكون فاضي هكذا ()
   ()=>{this.alertify.success('تم الاشتراك')},
   error=>{this.alertify.error(error)}
 )
  }
  cancel() {
   // console.log('ليس الأن');
    //emit من خلال هذا برسل الحدث
    this.CancelRegister.emit(false)
  }

}
