import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class SnackBarServices {
  constructor(private _snackBar: MatSnackBar) { }

  openSnackBar(message: string, action: string = 'ok') {
    this._snackBar.open(message, action, {
      duration: 2000,
      verticalPosition: 'top'
    });
  }

  Open(
    message: string,
    action: string = 'ok',
    duration: number = 2000,
    verticalPosition: 'top' | 'bottom' = 'top',  // Definido valor padrão
    horizontalPosition: 'start' | 'center' | 'end' | 'left' | 'right' = 'center'  // Definido valor padrão
  ) {
    this._snackBar.open(
      message,
      action,
      {
        duration,
        verticalPosition,
        horizontalPosition
      }
    );
  }
}
