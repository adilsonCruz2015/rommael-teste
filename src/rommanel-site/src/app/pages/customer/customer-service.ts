import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { first, Observable } from "rxjs";

import { Customer } from "./customer.model";
import { environment } from "../../../environments/environment";
import { ResultModel } from "../../../core/model/result.model";


@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private _http: HttpClient) { }

  create(record: Customer) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    });

    let body = JSON.parse(JSON.stringify(record));
    return this._http.post<Customer>(`${environment.api}/customers`, body, { headers: headers }).pipe(first());
  }

  get(queryField: string, pageNumber = 1, pageSize = 50): Observable<ResultModel<Customer>> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    });
    return this._http.get<ResultModel<Customer>>(`${environment.api}/customers`, {
      params: {
        queryField,
        pageNumber,
        pageSize
      },
      headers: headers
    });
  }

  remove(id: number) {
    return this._http.delete(`${environment.api}/customers/${id}`).pipe(first());
  }

  updateBroker(id: number, data: Customer): Observable<any> {
    return this._http.put(`${environment.api}/customers/${id}`, data).pipe(first());
  }
}