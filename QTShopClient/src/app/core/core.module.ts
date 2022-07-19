import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import {AppMaterialModule} from "../app-material.module";
import { HomeComponent } from './home/home.component';
import {RouterModule} from "@angular/router";



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
    AppMaterialModule,
    RouterModule
  ]
})
export class CoreModule { }
