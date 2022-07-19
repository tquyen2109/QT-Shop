import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogComponent } from './catalog.component';
import {CatalogRoutingModule} from "./catalog-routing.module";
import { FilterMenuComponent } from './filter-menu/filter-menu.component';
import { ProductCardComponent } from './product-card/product-card.component';
import {AppMaterialModule} from "../../app-material.module";


@NgModule({
  declarations: [
    CatalogComponent,
    FilterMenuComponent,
    ProductCardComponent
  ],
  imports: [
    CommonModule,
    CatalogRoutingModule,
    AppMaterialModule
  ]
})
export class CatalogModule { }
