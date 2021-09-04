import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from "ag-grid-angular";
import { PropertyRoutingModule } from './property-routing.module';
import { PropertyListComponent } from './property-list/property-list.component';
import { PropertyActionComponent } from './property-list/property-action/property-action.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    PropertyListComponent,
    PropertyActionComponent
  ],
  imports: [
    CommonModule,
    PropertyRoutingModule,
    SharedModule,
    AgGridModule.withComponents([
      PropertyActionComponent
    ])
  ]
})
export class PropertyModule { }
