import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseEntity } from '../models/base';
import { PageRequest } from '../models/PageRequest';
import { PageResponse } from '../models/pageResponse';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  uri: string;
  protected pageRequest: PageRequest;
  httpParams: HttpParams;
  private httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
  };
  constructor(private httpClient: HttpClient) {
    this.uri = environment.serviceBaseUrl;
    this.pageRequest = new PageRequest();
    this.httpParams = new HttpParams();
  }

  protected get<T>(actionName: string, request?: Object): Observable<T> {
    const compliedUrl = `${this.uri}/${actionName}`;
    return this.httpClient.get<T>(compliedUrl);
  }

  protected getAll<T extends BaseEntity>(
    actionName: string
  ): Observable<PageResponse<T>> {
    this.httpParams = new HttpParams();
    this.httpParams = this.httpParams.append(
      "PageRequest",
      JSON.stringify(this.pageRequest)
    );
    const compliedUrl = `${this.uri}/${actionName}`;
    return this.httpClient.get<PageResponse<T>>(compliedUrl, {
      params: this.httpParams
    });
  }

  protected getById<T extends BaseEntity>(id: string, actionName: string): Observable<T> {
    const compliedUrl = `${this.uri}/${actionName}/${id}`;
    return this.httpClient.get<T>(compliedUrl);
  }

  protected create<T extends BaseEntity>(t: T, actionName: string): Observable<T> {
    this.updateBaseEntityProperties(t);
    const compliedUrl = `${this.uri}/${actionName}`;
    return this.httpClient.post<T>(
      compliedUrl,
      JSON.stringify(t),
      this.httpOptions
    );
  }

  protected update<T extends BaseEntity>(t: T, actionName: string): Observable<T> {
    t.modifiedOn = new Date();
    const compliedUrl = `${this.uri}/${actionName}/${t.id}`;
    return this.httpClient.put<T>(
      compliedUrl,
      JSON.stringify(t),
      this.httpOptions
    );
  }


  protected updateBaseEntityProperties<T extends BaseEntity>(t: T) {
    t.createdOn = new Date();
    t.modifiedOn = new Date();
  }
}
