import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { Customer } from '../../customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatSortModule
  ],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss',
  providers: []
})
export class CustomerListComponent implements OnInit {

  @Input() set customers(value: Customer[]) {
    this._customers = value;
    console.log(this._customers);
    if (this.dataSource) {
      this.dataSource.data = this._customers;
    }
  }

  get customers(): Customer[] {
    return this._customers;
  }

  private _customers: Customer[] = [];
  @Output() edit: EventEmitter<Customer> = new EventEmitter(false);
  @Output() remove: EventEmitter<Customer> = new EventEmitter(false);

  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource: MatTableDataSource<Customer> = new MatTableDataSource();

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this._customers);
  }

  onEdit(record: Customer) {
    this.edit.emit(record);
  }

  onRemove(record: Customer) {
    this.remove.emit(record);
  }
}
