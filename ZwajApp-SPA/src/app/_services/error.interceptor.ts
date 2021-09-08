import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
 
@Injectable()
//  هذه الدالة مهمتها تعمل 
// كمفتش  لل ركوست وللرسبونس 

export class ErrorInterceptor implements HttpInterceptor{
    intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
        return next.handle(req).pipe(
            catchError(error =>{

                  //Application Error
                if(error instanceof HttpErrorResponse){
                    const applicactionError = error.headers.get('Application-Error');
                    if(applicactionError){
                       console.error(applicactionError);
                       return throwError(applicactionError);
                    }
                    //ModelStat Error
                    const serverError=error.error;
                    let modelStateErrors='';
                    if (serverError && typeof serverError==='object'){
                        for(const key in serverError){
                            if(serverError[key]){
                            modelStateErrors+=serverError[key]+'\n';
                            }

                        }
                    }

                    // Unauthorized error
                   if(error.status===401){
                       return throwError(error.statusText);
                   }

                    return throwError(modelStateErrors || serverError || 'server Error')
                }
            })
        )
    }
}
// 
export const ErrorInerceptorProvider={
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptor,
    multi:true // يعني حيشتغل  الديفلت وحيشتغل هذا لكلاس
}