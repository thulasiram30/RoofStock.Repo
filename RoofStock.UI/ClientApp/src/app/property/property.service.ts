import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { switchMap, map } from 'rxjs/operators';
import { Property } from '../core/models/property';
import { BaseService } from '../core/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class PropertyService extends BaseService {

  private propertyListRequest$ = new BehaviorSubject("");

  constructor(private http: HttpClient,) {
    super(http)
  }

  updatePropertyListRequest() {
    this.propertyListRequest$.next("");
  }

  propertyList$: Observable<Property[] | undefined> = this.propertyListRequest$.pipe(
    switchMap(() => this.getPropertyList())
  );

  getPropertyList(): Observable<Property[]> {
    return this.getAll('property')
      .pipe(
        map((px) => {
          return px.results as Property[];
        })
      )
  }

  updateProperty(property: Property): Observable<Property> {
    return this.update(property, "property");
  }

}
