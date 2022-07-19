import {Injectable} from "@angular/core";
import {DemoService} from "../../features/demo/services/demo.service";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {loadDemoData, loadDemoDataSuccess} from "../actions/demo.actions";
import {concatMap, map} from "rxjs";
import {CatalogService} from "../../features/catalog/catalog-service.service";
import {loadProducts, loadProductsSuccess} from "../actions/catalog.actions";

@Injectable()
export class CatalogEffects {
  constructor(private catalogService: CatalogService, private actions$: Actions) {
  }

  loadProducts$ = createEffect(() => this.actions$.pipe(
    ofType(loadProducts),
    concatMap(() => this.catalogService.getProducts().pipe(
      map(data => loadProductsSuccess({data}))
    )))
  );
}
