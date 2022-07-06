import {createAction, props} from "@ngrx/store";
import {DemoModel} from "../../features/demo/models/DemoModel";

export const loadDemoData = createAction(
  '[DemoModule] Load demo data'
);

export const loadDemoDataSuccess = createAction(
  '[DemoModule] Load demo data success',
  props<{data: DemoModel[]}>()
);

