import { Routes } from '@angular/router';
import { CustomerComponent } from './pages/customer/containers/customer/customer.component';
import { NotFoundComponent } from '../core/components/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: CustomerComponent
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: 'not-found' }  //
];
