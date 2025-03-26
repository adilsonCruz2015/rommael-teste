import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-filter-input',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './filter-input.component.html',
  styleUrl: './filter-input.component.scss'
})
export class FilterInputComponent {
  @Input() label: string = 'Filtro';
  @Input() queryField: FormControl = new FormControl('');
  @Output() filterChange = new EventEmitter<string>();

  clearFilter(): void {
    this.queryField.setValue('');
    this.filterChange.emit('');
  }

  onInputChange(): void {
    this.filterChange.emit(this.queryField.value);
  }
}
