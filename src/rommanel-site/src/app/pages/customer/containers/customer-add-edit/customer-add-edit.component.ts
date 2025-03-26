import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NgxMaskDirective } from 'ngx-mask';

import { Customer } from '../../customer.model';
import { CustomerService } from '../../customer-service';
import { SnackBarServices } from '../../../../../core/services/snack.bar.services';

@Component({
  selector: 'app-customer-add-edit',
  standalone: true,
  imports: [
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskDirective,
    MatCheckboxModule,
    FormsModule
  ],
  templateUrl: './customer-add-edit.component.html',
  styleUrl: './customer-add-edit.component.scss'
})
export class CustomerAddEditComponent implements OnInit {

  customerForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private _customerService: CustomerService,
    private _snackBar: SnackBarServices,
    private _dialogRef: MatDialogRef<CustomerAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Customer) { }

  ngOnInit(): void {

    this.customerForm = this.formBuilder.group({
      id: [this.data?.id || ''],
      name: [this.data?.name || '', [Validators.required]],
      document: [this.data?.document || '', [Validators.required]],
      birthDate: [this.data?.birthDate || ''],
      phone: [this.data?.phone || ''],
      email: [this.data?.email || ''],
      zipCode: [this.data?.address.zipCode || ''],
      street: [this.data?.address.street || ''],
      number: [this.data?.address.number || ''],
      neighborhood: [this.data?.address.neighborhood || ''],
      city: [this.data?.address.city || ''],
      state: [this.data?.address.state || ''],
      stateRegistration: [this.data?.stateRegistration || ''],
      taxExempt: [this.data?.taxExempt || ''],
    });
  }

  onFormSubmit() { }

  private onSuccess(message: string) {
    this._snackBar.openSnackBar(message, 'done');
    this._dialogRef.close(true);
  }

  getErrorMessage(fieldName: string) {
    const field = this.customerForm.get(fieldName);

    if (field?.hasError('required')) {
      return 'Campo obrigatório';
    }

    return 'Campo inválido';
  }
}
