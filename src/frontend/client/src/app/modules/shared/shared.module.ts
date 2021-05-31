import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { LAYOUT_COMPONENTS } from './layout/';

@NgModule({
  declarations: [
    LAYOUT_COMPONENTS
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    LAYOUT_COMPONENTS,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }