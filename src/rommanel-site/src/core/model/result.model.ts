
import { NotificationModel } from "./notification.model";

export interface ResultModel<T> {
  success?: boolean;
  data?: T[];
  pageNumber?: number;
  pageSize?: number;
  totalRecords?: number;
  totalPages?: number;
  notifications?: NotificationModel[]
}
