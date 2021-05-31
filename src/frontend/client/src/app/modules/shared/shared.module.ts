import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { LAYOUT_COMPONENTS } from './layout';
import { PIPES } from './pipes';

@NgModule({
  declarations: [
    ...LAYOUT_COMPONENTS,
    ...PIPES
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    ...LAYOUT_COMPONENTS,
    ...PIPES,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SharedModule { }