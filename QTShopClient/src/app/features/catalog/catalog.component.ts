import { Component, OnInit } from '@angular/core';
import {Store} from "@ngrx/store";
import {IAppState} from "../../store/app.interface";
import {Observable} from "rxjs";
import {Product} from "./models/Product";
import {loadProducts} from "../../store/actions/catalog.actions";
import {selectProducts} from "../../store/selectors/catalog.selectors";

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  products$: Observable<Product[]>;
  constructor(private store: Store<IAppState>) {
    this.products$ = this.store.select(selectProducts);
  }

  ngOnInit(): void {
    this.store.dispatch(loadProducts());
  }

}
