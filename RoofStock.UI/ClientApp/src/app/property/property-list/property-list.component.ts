import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { async } from '@angular/core/testing';
import { GridOptions, ColDef, NewValueParams, GridReadyEvent, ITooltipParams, ValueFormatterParams, RowNode }
  from "ag-grid-community";
import { Observable, of } from 'rxjs';
import { Property } from 'src/app/core/models/property';
import { PropertyService } from '../property.service';
import { PropertyActionComponent } from './property-action/property-action.component';
@Component({
  selector: 'roofstock-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss']
})
export class PropertyListComponent implements OnInit {

  gridOptions = this.initializeRoleGridOptions();
  constructor(private propertyService: PropertyService) { }

  propertyList$ : Observable<any[] | undefined> = this.propertyService.propertyList$;
  ngOnInit(): void {
    this.propertyService.updatePropertyListRequest();
  }

  private initializeRoleGridOptions() {
    return <GridOptions>{
      editType: "fullRow",
      columnDefs: this.getGridColumnDefs(),
      suppressClickEdit: true,
      suppressContextMenu: true,
      getRowNodeId: function (data: any) {
        return data.id;
      },
      frameworkComponents: {
        actionRenderer: PropertyActionComponent
      },
      onGridReady(params: GridReadyEvent) {
        if (params.api) {
          params.api.sizeColumnsToFit();
        }
      },
      rowSelection: "single",
      rowHeight: 55,
      headerHeight: 55,
      context: { componentParent: this },
      suppressRowClickSelection: true,
      paginationPageSize: 10
    }
  }

  getGridColumnDefs() {
    return [
      <ColDef>{
        headerName: "Year Built",
        field: "yearBuilt",
        editable: true,
        filter: 'agNumberColumnFilter'
      },
      <ColDef>{
        headerName: "List Price",
        field: "listPrice",
        editable: true,
        filter: 'agNumberColumnFilter'
      },
      <ColDef>{
        headerName: "Monthly Rent",
        field: "monthlyRent",
        editable: true,
        filter: 'agNumberColumnFilter'
      },
      <ColDef>{
        headerName: "Gross Yield%",
        field: "grossYieldPercentage",
      },
      {
        headerName: "Created On",
        field: "createdOn",
        valueFormatter: this.dateFormatter
      },
      {
        headerName: "Modified On",
        field: "modifiedOn",
        valueFormatter: this.dateFormatter
      },
      {
        cellRenderer: "actionRenderer",
        cellStyle: { "text-align": "right" }
      }
    ];
  }

  editProperty(rowIndex: number) {
    this.gridOptions.api ? this.gridOptions.api.startEditingCell({
      rowIndex: rowIndex,
      colKey: "yearBuilt"
    }) : null;
  }

  updateProperty(node: RowNode) {
    this.propertyService.updateProperty(node.data)
      .subscribe((p) => this.propertyService.updatePropertyListRequest());
  }

  dateFormatter(data: ValueFormatterParams) {
    return data.value ? new DatePipe('en-US').transform(data.value, 'short') : null;
  }

}
