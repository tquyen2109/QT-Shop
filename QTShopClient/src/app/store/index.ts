import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import {IAppState} from "./app.interface";
import {DemoReducer} from "./reducers/demo.reducer";
import {CatalogReducer} from "./reducers/catalog.reducer";


export const reducers: ActionReducerMap<IAppState> = {
  DemoState: DemoReducer,
  CatalogState: CatalogReducer
};
export const metaReducers: MetaReducer<IAppState>[] = [];
