import { Component, OnInit } from '@angular/core';
import { ICellRendererParams } from "ag-grid-community";

@Component({
  selector: 'roofstock-property-action',
  templateUrl: './property-action.component.html',
  styleUrls: ['./property-action.component.scss']
})
export class PropertyActionComponent implements OnInit {

  params!: ICellRendererParams;
  public isNew: any;
  constructor() {
    this.isNew = true;
  }

  ngOnInit(): void {
  }

  agInit(params: ICellRendererParams) {
    this.params = params;
  }

  edit() {
    this.isNew = false
    this.params.api.stopEditing(true);
    this.params.context.componentParent.editProperty(this.params.node.rowIndex);
  }

  update() {
    this.isNew = true
    this.params.api.stopEditing();
    this.params.context.componentParent.updateProperty(this.params.node);
  }

  cancel() {
    this.isNew = true
    this.params.api.stopEditing(true);
  }

}
