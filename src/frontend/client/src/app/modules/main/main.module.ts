import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main.router';

import { COMPONENTS } from './index';

@NgModule({
  declarations: [
    COMPONENTS
  ],
  imports: [
    CommonModule,
    MainRoutingModule
  ],
  providers: [

  ],
  exports: [
    COMPONENTS
  ]
})
export class MainModule { }
