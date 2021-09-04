import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { throwError } from "rxjs/internal/observable/throwError";
import { catchError } from 'rxjs/operators';
import { ErrorMessageService } from "./error-message.service";

@Injectable()
export class Interceptor implements HttpInterceptor {
  /**
   *
   */
  constructor(private errorMessageService: ErrorMessageService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      setHeaders: {
        Authorization: "Bearer ", // token
        'Access-Control-Allow-Origin': '*'
      }
    });
    return next.handle(req)
    .pipe(
      catchError(e => {
        this.errorMessageService.showErrorMessage(e);
        return throwError(e);
      })
    );
  }
  handleError(handleError: any): import("rxjs").OperatorFunction<HttpEvent<any>, any> {
    throw new Error("Method not implemented.");
  }

}
