import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {HomeComponent} from "./core/home/home.component";

const routes: Routes = [
  { path: '', component: HomeComponent},
  {
    path: 'catalog',
    loadChildren: () =>
      import('./features/catalog/catalog.module').then((mod) => mod.CatalogModule),
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
