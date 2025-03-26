import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { delay, finalize } from "rxjs";
import { LoadingService } from "../services/LoadingService";

export const networkInterceptorFn: HttpInterceptorFn = (req, next) => {
  let totalRequests = 0;
  let requestsCompleted = 0;

  const loadingService = inject(LoadingService);

  loadingService.setLoading(true);
  totalRequests++;

  return next(req).pipe(
    delay(200),
    finalize(() => {
      requestsCompleted++;

      if (requestsCompleted === totalRequests) {
        loadingService.setLoading(false);
        totalRequests = 0;
        requestsCompleted = 0;
      }
    })
  );
};
