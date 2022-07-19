import {createAction, props} from "@ngrx/store";
import {Product} from "../../features/catalog/models/Product";

export const loadProducts = createAction(
  '[Catalog] Load products'
)

export const loadProductsSuccess = createAction(
  '[Catalog] Load products success',
  props<{data: Product[]}>()
)

