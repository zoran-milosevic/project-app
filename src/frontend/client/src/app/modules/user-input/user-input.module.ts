import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserInputRoutingModule } from './user-input.router';

import { COMPONENTS } from './index';


@NgModule({
  declarations: [
    COMPONENTS
  ],
  imports: [
    CommonModule,
    UserInputRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [

  ],
  exports: [
    COMPONENTS
  ]
})
export class UserInputModule { }
