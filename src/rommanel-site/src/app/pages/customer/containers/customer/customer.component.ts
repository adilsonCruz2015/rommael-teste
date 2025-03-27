import { Component, OnInit } from '@angular/core';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';

import { catchError, debounceTime, distinctUntilChanged, Observable, of, switchMap, tap } from 'rxjs';

import { environment } from '../../../../../environments/environment';
import { CustomerService } from '../../customer-service';
import { SnackBarServices } from '../../../../../core/services/snack.bar.services';

import { ResultModel } from '../../../../../core/model/result.model';
import { Customer } from '../../customer.model';
import { ConfirmationDialogComponent } from '../../../../../core/components/confirmation-dialog/confirmation-dialog.component';
import { MessageGeneral } from '../../../../../core/messages/messages-general';
import { CustomerAddEditComponent } from '../customer-add-edit/customer-add-edit.component';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerListComponent } from '../../components/customer-list/customer-list.component';


@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginator,
    MatDialogModule,
    MatButtonModule,
    CommonModule,
    CustomerListComponent,
    MatToolbarModule,
    FormsModule
  ],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.scss'
})
export class CustomerComponent implements OnInit {

  searchResults$!: Observable<any>;
  pageIndex = 0;
  pageSize = environment.itensPerPage;
  queryField = new FormControl('');

  constructor(private _customerService: CustomerService,
    public _dialog: MatDialog,
    private _snackBar: SnackBarServices
  ) { }

  ngOnInit() {
    this.refresh();

    // Fluxo para monitorar mudanças no campo de busca e aplicar o filtro
    this.queryField.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(value => {
          this.pageIndex = 0; // Reseta a página para 0 em cada nova pesquisa
          return this.refresh({
            length: 0,
            pageIndex: this.pageIndex,
            pageSize: this.pageSize
          });
        })
      ).subscribe();
  }

  refresh(pageEvent: PageEvent = { length: 0, pageIndex: 0, pageSize: environment.itensPerPage })
    : Observable<ResultModel<Customer>> {
    const searchTerm = this.queryField.value?.trim() || '';

    this.searchResults$ = this._customerService.get(
      searchTerm,
      pageEvent.pageIndex,
      pageEvent.pageSize,
    ).pipe(
      tap((result) => {
        this.pageIndex = pageEvent.pageIndex;
        this.pageSize = pageEvent.pageSize;
      }),
      catchError(() => {
        return of({ customer: [], totalRecords: 0 } as ResultModel<Customer>);
      })
    );
    return this.searchResults$;
  }

  openAddEditEmpForm(customer: Customer | null) {
    const dialogRef = this._dialog.open(CustomerAddEditComponent, {
      width: '80%',  // Ajuste a largura para 80% da tela
      height: '650px', // Ajuste a altura fixa para 600px, por exemplo
      maxWidth: '90vw', // Defina um limite máximo para a largura
      maxHeight: '78vh', // Defina um limite máximo para a altura
      data: customer
    });
    this.updateList(dialogRef);
  }

  remove(customer: Customer) {
    const dialogRef = this._dialog.open(ConfirmationDialogComponent, {
      data: MessageGeneral.askCustomerExclusion,
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this._customerService.remove(customer.id!).subscribe({
          next: (res) => {
            this.refresh();
            this._snackBar.Open(MessageGeneral.customerExclusion, 'X', 5000);
          },
          error: () => console.log
        });
      }
    });
  }

  updateList(dialogRef: MatDialogRef<CustomerAddEditComponent>) {
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.refresh();
        }
      }
    });
  }
}
