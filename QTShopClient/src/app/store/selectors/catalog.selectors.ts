import {createFeatureSelector, createSelector} from "@ngrx/store";
import {CatalogState} from "../reducers/catalog.reducer";
export const featureKey = 'CatalogState';

export const selectFeature = createFeatureSelector<CatalogState>(featureKey);
export const selectProducts = createSelector(
  selectFeature,
  state => {return state.data}
);
