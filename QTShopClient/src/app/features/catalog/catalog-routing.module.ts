import { NgModule } from '@angular/core';
import {CatalogComponent} from "./catalog.component";
import {RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  {path: '', component: CatalogComponent},
]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CatalogRoutingModule { }
