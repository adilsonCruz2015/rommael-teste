import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideEnvironmentNgxMask } from 'ngx-mask';
import { provideHttpClient, withFetch, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { ErrorInterceptor } from '../core/interceptors/error.interceptor';
import { networkInterceptorFn } from '../core/interceptors/spinner.interceptor';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { getBrPaginatorIntl } from '../core/helper/br-paginator-intl';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }),
  provideRouter(routes),
  provideAnimationsAsync(),
  provideEnvironmentNgxMask(),
  provideHttpClient(
    withFetch(),
    withInterceptorsFromDi(),
    withInterceptors([ErrorInterceptor, networkInterceptorFn])
  ),
  {
    provide: MatPaginatorIntl,
    useFactory: getBrPaginatorIntl // tradutor a paginação para português Brasil.
  }
  ]
};
