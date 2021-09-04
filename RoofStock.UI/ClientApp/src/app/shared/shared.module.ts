import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  exports: [
    MaterialModule,
    FormsModule
  ]
})
export class SharedModule { }
