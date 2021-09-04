import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorMessageService {

  constructor() { }

  showErrorMessage(error: Error | HttpErrorResponse) {
    if (error instanceof Error) {
      console.exception(error.message);
    } else if (error instanceof HttpErrorResponse) {
      console.error(error);
    }
  }
}
