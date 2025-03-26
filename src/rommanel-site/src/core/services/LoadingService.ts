import { Injectable } from '@angular/core';

import { BehaviorSubject, delay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  public loading = new BehaviorSubject<boolean>(false);
  private _noRecordsFound = new BehaviorSubject<boolean>(false);
  public readonly noRecordsFound = this._noRecordsFound.asObservable();

  public setLoading(loading: boolean) {
    this.loading.next(loading);
  }

  showNoRecords(): void {
    this._noRecordsFound.next(true);
  }

  hideNoRecords(): void {
    this._noRecordsFound.next(false);
  }
}
