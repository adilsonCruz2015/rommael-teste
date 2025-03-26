import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest
} from "@angular/common/http";
import { inject } from "@angular/core";

import { catchError, Observable, throwError } from "rxjs";

import { ErrorHandlingService } from "../services/error-handling.service";
import { ErrorResponse } from "../model/error.model";

export const ErrorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {

  const errorService = inject(ErrorHandlingService);

  return next(req).pipe(
    catchError((error: any) => {
      if (error instanceof HttpErrorResponse) {

        switch (error.status) {
          case 404:
            errorService.handleNotFoundError();
            break;
          case 500:
            const body = error.error as ErrorResponse;
            errorService.handleServerError(body.detail!);
            break;
          case 400:
            errorService.handleBadRequestError(error);
            break;
        }

        throwError(() => error);
      }

      throw error;
    })
  );
}
