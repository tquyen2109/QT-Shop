import {IDemo} from "./reducers/demo.reducer";
import {CatalogState} from "./reducers/catalog.reducer";

export interface IAppState {
  DemoState: IDemo;
  CatalogState: CatalogState
}

