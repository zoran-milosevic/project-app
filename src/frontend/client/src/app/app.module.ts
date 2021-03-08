import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { RoutingModule } from './routing.module';
import { APP_MODULES } from '.';

import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { FooterComponent } from './components/footer/footer.component';

@NgModule({
  declarations: [
    HomeComponent,
    NavigationComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    RoutingModule,
    HttpClientModule,
    APP_MODULES
  ],
  providers: [

  ],
  bootstrap: [HomeComponent]
})
export class AppModule { }
