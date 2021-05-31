import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { UserInputRoutingModule } from './user-input-routing.module';

import { COMPONENTS } from './index';

@NgModule({
  declarations: [
    ...COMPONENTS
  ],
  imports: [
    CommonModule,
    UserInputRoutingModule,
    SharedModule
  ],
  providers: [

  ]
})
export class UserInputModule { }
