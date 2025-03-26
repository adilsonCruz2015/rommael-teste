import { Component, OnInit } from '@angular/core';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { MatMenuModule, MatMenuTrigger } from '@angular/material/menu';

import { MenuBarComponent } from '../menu/menu-bar/menu-bar.component';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../error-dialog/error-dialog.component';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatTooltip,
    RouterLink,
    MatMenuModule,
    MatDialogModule,
    MenuBarComponent
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent implements OnInit {

  constructor(public _dialog: MatDialog,
  ) { }

  ngOnInit(): void {

  }

  name(): string | null {
    return 'Teste';
  }

  openMyMenu(menuTrigger: MatMenuTrigger) {
    menuTrigger.openMenu();
  }

  onError(errorMsg: string) {
    this._dialog.open(ErrorDialogComponent, {
      data: errorMsg
    });
  }

}
