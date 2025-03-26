import { NotificationModel } from "./notification.model";


export interface ResponseModel {
  success?: string;
  notifications?: NotificationModel[];
}
