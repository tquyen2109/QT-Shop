import { Component, OnInit } from '@angular/core';
import {Observable} from "rxjs";
import {DemoModel} from "./models/DemoModel";
import {IAppState} from "../../store/app.interface";
import {Store} from "@ngrx/store";
import {loadDemoData} from "../../store/actions/demo.actions";
import {selectDemoData} from "../../store/selectors/demo.selectors";

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrls: ['./demo.component.css']
})
export class DemoComponent implements OnInit {
  data$: Observable<DemoModel[]>;
  constructor(private store: Store<IAppState>) {
    this.data$ = this.store.select(selectDemoData);
  }

  ngOnInit(): void {
    this.store.dispatch(loadDemoData());
  }

}
