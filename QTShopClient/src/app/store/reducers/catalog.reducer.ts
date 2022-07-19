import {createReducer, on} from "@ngrx/store";
import {Product} from "../../features/catalog/models/Product";
import {loadProductsSuccess} from "../actions/catalog.actions";


export const initialCatalogState: CatalogState= {
  data: []
};
export interface CatalogState {
  data: Product[]
}
export const CatalogReducer = createReducer(
  initialCatalogState as CatalogState,
  on(loadProductsSuccess, (state, {data}) => ({ ...state, data: data })),
);
