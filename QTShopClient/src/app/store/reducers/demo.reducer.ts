import { loadDemoDataSuccess} from "../actions/demo.actions";
import {createReducer, on} from "@ngrx/store";
import {DemoModel} from "../../features/demo/models/DemoModel";

export const initialDemoState: IDemo = {
  data: []
};
export interface IDemo {
  data: DemoModel[]
}
export const DemoReducer = createReducer(
  initialDemoState as IDemo,
  on(loadDemoDataSuccess, (state, {data}) => ({ ...state, data: data })),
);
