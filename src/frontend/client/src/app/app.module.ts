import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CoreModule } from '@core/core.module';
import { SharedModule } from '@shared/shared.module';
import { AppRoutingModule } from './app-routing.module';

import { HomeComponent } from '@shared/layout';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule
  ],
  bootstrap: [HomeComponent]
})
export class AppModule { }
