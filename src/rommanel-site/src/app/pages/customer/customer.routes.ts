import { Routes } from "@angular/router";

import { CustomerAddEditComponent } from "./containers/customer-add-edit/customer-add-edit.component";
import { CustomerComponent } from "./containers/customer/customer.component";

export const CUSTOMER_ROUTES: Routes = [
  { path: '', component: CustomerComponent },
  { path: 'new', component: CustomerAddEditComponent },
  {
    path: ':cliente/edit',
    component: CustomerAddEditComponent
  }
];