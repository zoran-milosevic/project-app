import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { FileRoutingModule } from './file.router';

import { COMPONENTS } from './index';

@NgModule({
  declarations: [
    COMPONENTS
  ],
  imports: [
    CommonModule,
    FileRoutingModule,
    SharedModule
  ],
  providers: [
    
  ],
  exports: [
    COMPONENTS
  ]
})
export class FileModule { }
