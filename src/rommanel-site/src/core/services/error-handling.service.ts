import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { HttpErrorResponse } from "@angular/common/http";

import { MatDialog } from "@angular/material/dialog";
import { ModalModel } from "../model/modal.model";
import { MessageGeneral } from "../messages/messages-general";
import { ErrorDialogComponent } from "../components/error-dialog/error-dialog.component";
import { ResponseModel } from "../model/response.model";
import { NotificationModel } from "../model/notification.model";



@Injectable({ providedIn: 'root' })
export class ErrorHandlingService {

  modalModel!: ModalModel;
  constructor(private dialog: MatDialog,
    private router: Router) { }


  handleNotFoundError(): void {
    const message = MessageGeneral.notFound;
    this.modalModel = new ModalModel(message, 'Notificação');
    const dialogRef = this.dialog.open(ErrorDialogComponent, {
      data: this.modalModel
    });

    dialogRef.afterClosed().subscribe(() => {
      this.router.navigate(['/not-found']);
    });
  }

  handleServerError(detail: string): void {
    const messages = `${detail}. ${MessageGeneral.openCall}`;
    this.modalModel = new ModalModel(messages, 'Houve um erro');
    this.dialog.open(ErrorDialogComponent, {
      data: this.modalModel
    });
  }

  handleBadRequestError(error: HttpErrorResponse) {
    const response = error.error as ResponseModel;
    if (response.notifications &&
      Array.isArray(response.notifications) &&
      response.notifications.length > 0) {
      const errorMessages = response.notifications
        .map((notification: NotificationModel) => notification.description)
        .join('\n');

      this.modalModel = new ModalModel(errorMessages, 'Notificação');
      this.dialog.open(ErrorDialogComponent, {
        data: this.modalModel
      });
    }
  }
}
