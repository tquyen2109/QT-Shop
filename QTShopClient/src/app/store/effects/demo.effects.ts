import {Injectable} from "@angular/core";
import {DemoService} from "../../features/demo/services/demo.service";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {loadDemoData, loadDemoDataSuccess} from "../actions/demo.actions";
import {concatMap, map} from "rxjs";

@Injectable()
export class DemoEffects {
  constructor(private demoService: DemoService, private actions$: Actions) {
  }

  loadData$ = createEffect(() => this.actions$.pipe(
    ofType(loadDemoData),
      concatMap(() => this.demoService.getDemoData().pipe(
        map(data => loadDemoDataSuccess({data}))
      )))
    );
}
