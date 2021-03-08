import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FileRoutingModule } from './file.router';

import { COMPONENTS } from './index';

@NgModule({
  declarations: [
    COMPONENTS
  ],
  imports: [
    CommonModule,
    FileRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    
  ],
  exports: [
    COMPONENTS
  ]
})
export class FileModule { }
