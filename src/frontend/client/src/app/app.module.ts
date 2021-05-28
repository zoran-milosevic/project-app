import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { RoutingModule } from './routing.module';

import { APP_COMPONENTS } from '.';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [
    APP_COMPONENTS
  ],
  imports: [
    BrowserModule,
    RoutingModule,
    HttpClientModule
  ],
  providers: [

  ],
  bootstrap: [HomeComponent]
})
export class AppModule { }
