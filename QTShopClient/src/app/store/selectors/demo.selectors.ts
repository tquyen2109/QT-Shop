import {IDemo} from "../reducers/demo.reducer";
import {createFeatureSelector, createSelector} from "@ngrx/store";
export const featureKey = 'DemoState';

export const selectFeature = createFeatureSelector<IDemo>(featureKey);
export const selectDemoData = createSelector(
  selectFeature,
  state => {return state.data}
);
