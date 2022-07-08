import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import {AppMaterialModule} from "../app-material.module";
import { HomeComponent } from './home/home.component';



@NgModule({
  declarations: [
    NavBarComponent,
    HomeComponent
  ],
  exports: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    AppMaterialModule
  ]
})
export class CoreModule { }
