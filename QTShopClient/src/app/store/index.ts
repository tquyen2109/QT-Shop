import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import {IAppState} from "./app.interface";
import {DemoReducer} from "./reducers/demo.reducer";


export const reducers: ActionReducerMap<IAppState> = {
  DemoState: DemoReducer
};
export const metaReducers: MetaReducer<IAppState>[] = [];
